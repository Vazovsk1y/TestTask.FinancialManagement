using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class MoneyAccountService : BaseService, IMoneyAccountService
{
	public MoneyAccountService(TestTaskDbContext dbContext, IUserProvider userProvider, IServiceScopeFactory serviceScopeFactory) : base(dbContext, userProvider, serviceScopeFactory)
	{
	}

	public async Task<Result<MoneyAccountId>> CreateAsync(CurrencyId currencyId, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<MoneyAccountId>(requesterIdResult.ErrorMessage);
		}

		var requester = await _dbContext
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
		_dbContext.MoneyAccounts.Add(moneyAccount);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return moneyAccount.Id;
	}

	public async Task<Result<IReadOnlyCollection<MoneyAccountDTO>>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<IReadOnlyCollection<MoneyAccountDTO>>(requesterIdResult.ErrorMessage);
		}

		var requester = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.GetById(requesterIdResult.Value);

		bool actionPermitted = requester!.IsInRole(DefaultRoles.Admin) || requester!.Id == userId;
		if (!actionPermitted)
		{
			return Result.Failure<IReadOnlyCollection<MoneyAccountDTO>>(Errors.Auth.AccessDenied);
		}

		var result = await _dbContext
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
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<MoneyAccountDTO>(requesterIdResult.ErrorMessage);
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
			return Result.Failure<MoneyAccountDTO>(Errors.Auth.AccessDenied);
		}

		var moneyAccount = await _dbContext
			.MoneyAccounts
			.AsNoTracking()
			.Include(e => e.Currency)
			.SingleOrDefaultAsync(e => e.Id == moneyAccountId, cancellationToken);

		return moneyAccount is null ? Result.Failure<MoneyAccountDTO>(Errors.EntityWithPassedIdIsNotExists(nameof(MoneyAccount))) : moneyAccount.ToDTO();
	}

	private async Task<Result<MoneyAccount>> CreateMoneyAccount(User user, CurrencyId currencyId)
	{
		if (user.MoneyAccounts.Any(e => e.CurrencyId == currencyId))
		{
			return Result.Failure<MoneyAccount>(Errors.MoneyAccount.AccountWithPassedCurrencyIsAlreadyCreated);
		}

		if (!await _dbContext.Currencies.AnyAsync(e => e.Id == currencyId))
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