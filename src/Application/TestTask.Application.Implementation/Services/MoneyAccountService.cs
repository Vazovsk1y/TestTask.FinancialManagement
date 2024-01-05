using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class MoneyAccountService(
	TestTaskDbContext dbContext) : IMoneyAccountService
{
	public async Task<Result<MoneyAccountId>> CreateAsync(UserId requesterId, CurrencyId currencyId, CancellationToken cancellationToken = default)
	{
		var requester = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.Include(e => e.MoneyAccounts)
			.GetById(requesterId);

		if (requester is null)
		{
			return Result.Failure<MoneyAccountId>(Errors.EntityWithPassedIdIsNotExists(nameof(User)));
		}

		var moneyAccountCreationResult = await CreateMoneyAccount(requester, currencyId);
		if (moneyAccountCreationResult.IsFailure)
		{
			return Result.Failure<MoneyAccountId>(moneyAccountCreationResult.ErrorMessage);
		}

		var moneyAccount = moneyAccountCreationResult.Value;
		dbContext.MoneyAccounts.Add(moneyAccount);
		await dbContext.SaveChangesAsync(cancellationToken);
		return moneyAccount.Id;
	}

	public async Task<Result<IReadOnlyCollection<MoneyAccountDTO>>> GetAllByUserIdAsync(UserId requesterId, CancellationToken cancellationToken = default)
	{
		var result = await dbContext
			.MoneyAccounts
			.Include(e => e.Currency)
			.Where(e => e.UserId == requesterId)
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);

		return result;
	}

	public Task<Result<MoneyAccountDTO>> GetByIdAsync(UserId requesterId, MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	private async Task<Result<MoneyAccount>> CreateMoneyAccount(User user, CurrencyId currencyId)
	{
		if (user.MoneyAccounts.Any(e => e.CurrencyId == currencyId))
		{
			return Result.Failure<MoneyAccount>(Errors.MoneyAccount.AccountWithPassedCurrencyIsAlreadyCreated);
		}

		if (!await dbContext.Currencies.AnyAsync(e => e.Id == currencyId))
		{
			return Result.Failure<MoneyAccount>(Errors.EntityWithPassedIdIsNotExists(nameof(Currency)));
		}

		return new MoneyAccount
		{
			Balance = 0m,
			CurrencyId = currencyId,
			UserId = user.Id,
		};
	}
}