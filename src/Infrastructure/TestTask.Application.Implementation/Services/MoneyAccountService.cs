using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Contracts;
using TestTask.Application.Implementation.Constants;
using TestTask.Application.Implementation.Extensions;
using TestTask.Application.Services;
using TestTask.Application.Shared;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class MoneyAccountService(
	TestTaskDbContext dbContext,
	IUserProvider userProvider,
	IServiceScopeFactory serviceScopeFactory)
	: BaseService(dbContext, userProvider, serviceScopeFactory), IMoneyAccountService
{
	public async Task<Result<MoneyAccountId>> CreateAsync(CurrencyId currencyId, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = UserProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<MoneyAccountId>(requesterIdResult.ErrorMessage);
		}

		var requester = await DbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.Include(e => e.MoneyAccounts)
			.GetById(requesterIdResult.Value);

		var moneyAccountCreationResult = await CreateMoneyAccount(requester!, currencyId);
		if (moneyAccountCreationResult.IsFailure)
		{
			return Result.Failure<MoneyAccountId>(moneyAccountCreationResult.ErrorMessage);
		}

		var moneyAccount = moneyAccountCreationResult.Value;
		DbContext.MoneyAccounts.Add(moneyAccount);
		await DbContext.SaveChangesAsync(cancellationToken);
		return moneyAccount.Id;
	}

	public async Task<Result<IReadOnlyCollection<MoneyAccountDTO>>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = UserProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<IReadOnlyCollection<MoneyAccountDTO>>(requesterIdResult.ErrorMessage);
		}

		var requester = await DbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.GetById(requesterIdResult.Value);

		bool actionPermitted = requester!.IsInRole(DefaultRoles.Admin) || requester!.Id == userId;
		if (!actionPermitted)
		{
			return Result.Failure<IReadOnlyCollection<MoneyAccountDTO>>(Errors.Auth.AccessDenied);
		}

		var result = await DbContext
			.MoneyAccounts
			.AsNoTracking()
			.Include(e => e.Currency)
			.Where(e => e.UserId == userId)
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);

		return result;
	}

	public async Task<Result<MoneyAccountDTO>> GetByIdAsync(MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = UserProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<MoneyAccountDTO>(requesterIdResult.ErrorMessage);
		}

		var requester = await DbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.Include(e => e.MoneyAccounts)
		    .GetById(requesterIdResult.Value);

		bool actionPermitted = requester!.IsInRole(DefaultRoles.Admin) || requester!.MoneyAccounts.Any(e => e.Id == moneyAccountId);
		if (!actionPermitted)
		{
			return Result.Failure<MoneyAccountDTO>(Errors.Auth.AccessDenied);
		}

		var moneyAccount = await DbContext
			.MoneyAccounts
			.AsNoTracking()
			.Include(e => e.Currency)
			.SingleOrDefaultAsync(e => e.Id == moneyAccountId, cancellationToken);

		return moneyAccount?.ToDTO() ?? Result.Failure<MoneyAccountDTO>(Errors.EntityWithPassedIdIsNotExists(nameof(MoneyAccount)));
	}

	private async Task<Result<MoneyAccount>> CreateMoneyAccount(User user, CurrencyId currencyId)
	{
		if (user.MoneyAccounts.Any(e => e.CurrencyId == currencyId))
		{
			return Result.Failure<MoneyAccount>(Errors.MoneyAccount.AccountWithPassedCurrencyIsAlreadyCreated);
		}

		if (!await DbContext.Currencies.AnyAsync(e => e.Id == currencyId))
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