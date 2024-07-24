using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Application.Shared;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.Domain.Enums;

namespace TestTask.Application.Implementation.Services;

internal class MoneyOperationService : BaseService, IMoneyOperationService
{
	private readonly IClock _clock;
	public MoneyOperationService(
		TestTaskDbContext dbContext, 
		IUserProvider userProvider, 
		IServiceScopeFactory serviceScopeFactory, 
		IClock clock) : base(dbContext, userProvider, serviceScopeFactory)
	{
		_clock = clock;
	}

	public async Task<Result<MoneyOperationId>> EnrollAsync(EnrollDTO enrollDTO, CancellationToken cancellationToken = default)
	{
		var validationResult = Validate(enrollDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<MoneyOperationId>(validationResult.ErrorMessage);
		}

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

	public async Task<Result<MoneyOperationId>> WithdrawalAsync(WithdrawalDTO withdrawalDTO, CancellationToken cancellationToken = default)
	{
		var validationResult = Validate(withdrawalDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<MoneyOperationId>(validationResult.ErrorMessage);
		}

		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<MoneyOperationId>(requesterIdResult.ErrorMessage);
		}

		var account = await _dbContext
			.MoneyAccounts
			.SingleOrDefaultAsync(e => e.Id == withdrawalDTO.MoneyAccountFromId, cancellationToken);

		if (account is null)
		{
			return Result.Failure<MoneyOperationId>(Errors.EntityWithPassedIdIsNotExists(nameof(MoneyAccount)));
		}

		var withdrawalCreationResult = CreateWithdrawal(requesterIdResult.Value, withdrawalDTO, account);
		if (withdrawalCreationResult.IsFailure)
		{
			return Result.Failure<MoneyOperationId>(withdrawalCreationResult.ErrorMessage);
		}

		var withdrawal = withdrawalCreationResult.Value;
		account.Balance -= withdrawal.MoneyAmount;

		_dbContext.MoneyOperations.Add(withdrawal);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return withdrawal.Id;
	}

	public async Task<Result<TransferResponseDTO>> TransferAsync(TransferDTO transferDTO, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<TransferResponseDTO>(requesterIdResult.ErrorMessage);
		}

		var validationResult = Validate(transferDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<TransferResponseDTO>(validationResult.ErrorMessage);
		}

		var fromAccount = await _dbContext.MoneyAccounts.SingleOrDefaultAsync(e => e.Id == transferDTO.MoneyAccountFromId, cancellationToken);
		if (fromAccount is null)
		{
			return Result.Failure<TransferResponseDTO>(Errors.EntityWithPassedIdIsNotExists(nameof(MoneyAccount)));
		}

		var toAccount = await _dbContext.MoneyAccounts.SingleOrDefaultAsync(e => e.Id == transferDTO.MoneyAccountToId, cancellationToken);
		if (toAccount is null)
		{
			return Result.Failure<TransferResponseDTO>(Errors.EntityWithPassedIdIsNotExists(nameof(MoneyAccount)));
		}

		var transfersCreationResult = CreateTransfers(requesterIdResult.Value, transferDTO, fromAccount, toAccount);
		if (transfersCreationResult.IsFailure)
		{
			return Result.Failure<TransferResponseDTO>(transfersCreationResult.ErrorMessage);
		}

		var (sub, add) = transfersCreationResult.Value;
		fromAccount.Balance -= sub.MoneyAmount;
		toAccount.Balance += add.MoneyAmount;
		_dbContext.MoneyOperations.AddRange(sub, add);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return new TransferResponseDTO(sub.Id, add.Id);
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

	public async Task<Result<IReadOnlyCollection<MoneyOperationDTO>>> GetAllByMoneyAccountIdAsync(MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default)
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
			.Include(e => e.MoneyAccounts)
		    .GetById(requesterIdResult.Value);

		bool actionPermitted = requester!.IsInRole(DefaultRoles.Admin) || requester!.MoneyAccounts.Any(e => e.Id == moneyAccountId);
		if (!actionPermitted)
		{
			return Result.Failure<IReadOnlyCollection<MoneyOperationDTO>>(Errors.Auth.AccessDenied);
		}

		var result = await _dbContext
			.MoneyOperations
			.AsNoTracking()
			.Where(e => e.MoneyAccountFromId == moneyAccountId || e.MoneyAccountToId == moneyAccountId)
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);

		return result;
	}

	private async Task<Result<MoneyOperation>> CreateEnrollment(EnrollDTO enrollDTO, MoneyAccount to)
	{
		if (!await _dbContext.Currencies.AnyAsync(e => e.Id == enrollDTO.CurrencyFromId))
		{
			return Result.Failure<MoneyOperation>(Errors.EntityWithPassedIdIsNotExists(nameof(Currency)));
		}

		decimal commission = enrollDTO.CurrencyFromId == to.CurrencyId ?
			decimal.Zero
			:
			_dbContext.Commissions.SingleOrDefault(e => e.CurrencyFromId == enrollDTO.CurrencyFromId && e.CurrencyToId == to.CurrencyId)?.Value ?? Commission.DefaultValue;

		decimal exchangeRate = enrollDTO.CurrencyFromId == to.CurrencyId ? 
			1m
			:
		    _dbContext.ExchangeRates.Single(e => e.CurrencyFromId == enrollDTO.CurrencyFromId && e.CurrencyToId == to.CurrencyId).Value;

		var finalAmountResult = CalculateFinalSum(enrollDTO.MoneyAmount, commission, exchangeRate, false);
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

	private Result<MoneyOperation> CreateWithdrawal(UserId requesterId, WithdrawalDTO withdrawalDTO, MoneyAccount from)
	{
		if (from.UserId != requesterId)
		{
			return Result.Failure<MoneyOperation>(Errors.Auth.AccessDenied);
		}

		if (from.Balance < withdrawalDTO.MoneyAmount)
		{
			return Result.Failure<MoneyOperation>("Not enough money on balance.");
		}

		return new MoneyOperation
		{
			MoneyAccountFromId = withdrawalDTO.MoneyAccountFromId,
			MoneyAmount = withdrawalDTO.MoneyAmount,
			OperationDate = _clock.GetUtcNow(),
			AppliedCommissionValue = decimal.Zero,
			AppliedExchangeRate = decimal.Zero,
			MoveType = MoneyMoveTypes.Subtracting,
			OperationType = MoneyOperationTypes.Withdrawal,
		};
	}

	private Result<(MoneyOperation transferSub, MoneyOperation transferAdd)> CreateTransfers(UserId requesterId, TransferDTO transferDTO, MoneyAccount from, MoneyAccount to)
	{
		if (from.UserId != requesterId)
		{
			return Result.Failure<(MoneyOperation, MoneyOperation)>(Errors.Auth.AccessDenied);
		}

		decimal commission = _dbContext.Commissions.SingleOrDefault(e => e.CurrencyFromId == from.CurrencyId && e.CurrencyToId == to.CurrencyId)?.Value ?? Commission.DefaultValue;

        decimal exchangeRate = from.CurrencyId == to.CurrencyId ?
            1m
            :
            _dbContext.ExchangeRates.Single(e => e.CurrencyFromId == from.CurrencyId && e.CurrencyToId == to.CurrencyId).Value;

        var finalAmountResult = CalculateFinalSum(transferDTO.MoneyAmount, commission, exchangeRate, true);
		if (finalAmountResult.IsFailure)
		{
			return Result.Failure<(MoneyOperation, MoneyOperation)> (finalAmountResult.ErrorMessage);
		}

		decimal finalAmount = finalAmountResult.Value;
		if (from.Balance < finalAmount)
		{
			return Result.Failure<(MoneyOperation, MoneyOperation)>("Not enough money on balance.");
		}

		var transferSub = new MoneyOperation
		{
			MoneyAccountFromId = from.Id,
			MoneyAccountToId = to.Id,
			AppliedCommissionValue = commission,
			AppliedExchangeRate = exchangeRate,
			OperationDate = _clock.GetUtcNow(),
			MoneyAmount = finalAmount,
			MoveType = MoneyMoveTypes.Subtracting,
			OperationType = MoneyOperationTypes.Transfer
		};

		var transferAdd = new MoneyOperation
		{
			MoneyAccountFromId = from.Id,
			MoneyAccountToId = to.Id,
			AppliedCommissionValue = decimal.Zero,
			AppliedExchangeRate = 1m,
			OperationDate = _clock.GetUtcNow(),
			MoneyAmount = transferDTO.MoneyAmount,
			MoveType = MoneyMoveTypes.Adding,
			OperationType = MoneyOperationTypes.Transfer
		};

		return (transferSub, transferAdd);
	}

	private static Result<decimal> CalculateFinalSum(decimal startAmount, decimal commission, decimal exchangeRate, bool addCommission)
	{
		decimal result = startAmount;
		if (exchangeRate != decimal.Zero)
		{
			result *= exchangeRate;
		}

		if (commission != decimal.Zero)
		{
			result = addCommission ? result + (result * commission) : result - (result * commission);
		}
		
		if (result <= 0)
		{
			return Result.Failure<decimal>("Too small money amount.");
		}

		return result;
	}
}