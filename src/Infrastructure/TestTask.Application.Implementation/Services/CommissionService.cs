﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Application.Shared;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class CommissionService : BaseService, ICommissionService
{
	public CommissionService(TestTaskDbContext dbContext, IUserProvider userProvider, IServiceScopeFactory serviceScopeFactory) : base(dbContext, userProvider, serviceScopeFactory)
	{
	}

	public async Task<Result<CommissionId>> AddAsync(CommissionAddDTO commissionAddDTO, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<CommissionId>(requesterIdResult.ErrorMessage);
		}

		var requester = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.GetById(requesterIdResult.Value);

		if (!requester!.IsInRole(DefaultRoles.Admin))
		{
			return Result.Failure<CommissionId>(Errors.Auth.AccessDenied);
		}

		var commissionCreationResult = await CreateCommission(commissionAddDTO);
		if (commissionCreationResult.IsFailure)
		{
			return Result.Failure<CommissionId>(commissionCreationResult.ErrorMessage);
		}

		var commission = commissionCreationResult.Value;
		_dbContext.Commissions.Add(commission);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return commission.Id;
	}

	public async Task<Result<IReadOnlyCollection<CommissionDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var result = await _dbContext
			.Commissions
			.AsNoTracking()
			.Include(e => e.To)
			.Include(e => e.From)
			.Select(e => new CommissionDTO(
				e.Id, e.From!.AlphabeticCode, e.To!.AlphabeticCode, e.Value
				))
			.ToListAsync(cancellationToken);

		return result;
	}

	private async Task<Result<Commission>> CreateCommission(CommissionAddDTO commissionAddDTO)
	{
		var validationResult = Validate(commissionAddDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<Commission>(validationResult.ErrorMessage);
		}

		bool isAlreadyExists = await _dbContext
			.Commissions
			.AnyAsync(e => e.CurrencyFromId == commissionAddDTO.CurrencyFromId && e.CurrencyToId == commissionAddDTO.CurrencyToId);

		if (isAlreadyExists)
		{
			return Result.Failure<Commission>("Commission with that currencies is already defined.");
		}

		var passedCurrencies = new List<CurrencyId>() { commissionAddDTO.CurrencyFromId, commissionAddDTO.CurrencyToId };
		var isAllCurrenciesExists = _dbContext.Currencies.Where(e => passedCurrencies.Contains(e.Id)).Count() == passedCurrencies.Count;
		if (!isAllCurrenciesExists)
		{
			return Result.Failure<Commission>("Any of passed currencies id is not exists.");
		}

		return new Commission
		{
			CurrencyFromId = commissionAddDTO.CurrencyFromId,
			CurrencyToId = commissionAddDTO.CurrencyToId,
			Value = commissionAddDTO.CommissionValue
		};
	}
}