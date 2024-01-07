using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.Domain.Enums;

namespace TestTask.Application.Implementation.Services;

internal class MoneyOperationService : BaseService, IMoneyOperationService
{
	private readonly IClock _clock;
	private readonly IExchangeRateProvider _exchangeRateProvider;
	public MoneyOperationService(
		TestTaskDbContext dbContext, 
		IUserProvider userProvider, 
		IServiceScopeFactory serviceScopeFactory, 
		IClock clock, 
		IExchangeRateProvider exchangeRateProvider) : base(dbContext, userProvider, serviceScopeFactory)
	{
		_clock = clock;
		_exchangeRateProvider = exchangeRateProvider;
	}

	public async Task<Result<MoneyOperationId>> EnrollAsync(EnrollDTO enrollDTO, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<MoneyOperationId>(requesterIdResult.ErrorMessage);
		}

		var account = await _dbContext
			.MoneyAccounts
			.SingleOrDefaultAsync(e => e.Id == enrollDTO.MoneyAccountToId, cancellationToken);

		if (account is null)
		{
			return Result.Failure<MoneyOperationId>(Errors.EntityWithPassedIdIsNotExists(nameof(MoneyAccount)));
		}

		var enrollmentCreationResult = await CreateEnrollment(enrollDTO, account);
		if (enrollmentCreationResult.IsFailure)
		{
			return Result.Failure<MoneyOperationId>(enrollmentCreationResult.ErrorMessage);
		}

		var enrollment = enrollmentCreationResult.Value;
		account.Balance += enrollment.MoneyAmount;

		_dbContext.MoneyOperations.Add(enrollment);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return enrollment.Id;
	}

	public async Task<Result<IReadOnlyCollection<MoneyOperationDTO>>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<IReadOnlyCollection<MoneyOperationDTO>>(requesterIdResult.ErrorMessage);
		}

		var requester = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.GetById(requesterIdResult.Value);

		bool actionPermitted = requester!.IsInRole(DefaultRoles.Admin) || requester!.Id == userId;
		if (!actionPermitted)
		{
			return Result.Failure<IReadOnlyCollection<MoneyOperationDTO>>(Errors.Auth.AccessDenied);
		}

		var accounts =  _dbContext
			.MoneyAccounts
			.AsNoTracking()
			.Where(e => e.UserId == userId)
			.Select(e => e.Id);

		var result = await _dbContext
			.MoneyOperations
			.AsNoTracking()
			.Where(e => accounts.Contains(e.MoneyAccountFromId) || accounts.Contains(e.MoneyAccountToId))
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);


		return result;
	}

	public Task<Result<IReadOnlyCollection<MoneyOperationDTO>>> GetAllByMoneyAccountIdAsync(UserId requesterId, MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<MoneyOperationId>> TransferAsync(UserId requesterId, TransferDTO transferDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<MoneyOperationId>> WithdrawalAsync(UserId requesterId, WithdrawalDTO withdrawalDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	private async Task<Result<MoneyOperation>> CreateEnrollment(EnrollDTO enrollDTO, MoneyAccount moneyAccountTo)
	{
		var validationResult = Validate(enrollDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<MoneyOperation>(validationResult.ErrorMessage);
		}

		if (!await _dbContext.Currencies.AnyAsync(e => e.Id == enrollDTO.CurrencyFromId))
		{
			return Result.Failure<MoneyOperation>(Errors.EntityWithPassedIdIsNotExists(nameof(Currency)));
		}

		decimal commission = _dbContext.Commissions.SingleOrDefault(e => e.CurrencyFromId == enrollDTO.CurrencyFromId && e.CurrencyToId == moneyAccountTo.CurrencyId)?.Value ?? Commission.DefaultValue;
		decimal exchangeRate = enrollDTO.CurrencyFromId == moneyAccountTo.CurrencyId ? decimal.Zero : _exchangeRateProvider.GetRate(enrollDTO.CurrencyFromId, moneyAccountTo.CurrencyId).Value;


		var finalAmountResult = CalculateFinalSum(enrollDTO.MoneyAmount, commission, exchangeRate);
		if (finalAmountResult.IsFailure)
		{
			return Result.Failure<MoneyOperation>(finalAmountResult.ErrorMessage);
		}

		decimal finalAmount = finalAmountResult.Value;
		return new MoneyOperation
		{
			AppliedExchangeRate = exchangeRate,
			AppliedCommissionValue = commission,
			MoneyAmount = finalAmount,
			OperationDate = _clock.GetUtcNow(),
			MoneyAccountToId = enrollDTO.MoneyAccountToId,
			MoveType = MoneyMoveTypes.Adding,
			OperationType = MoneyOperationTypes.Enrolment
		};
	}

	private static Result<decimal> CalculateFinalSum(decimal startAmount, decimal commission, decimal exchangeRate)
	{
		decimal result = startAmount;
		if (exchangeRate != decimal.Zero)
		{
			result *= exchangeRate;
		}

		if (commission != decimal.Zero)
		{
			result -= (result * commission);
		}
		
		if (result <= 0)
		{
			return Result.Failure<decimal>("Final money amount must be greater than zero.");
		}

		return result;
	}
}