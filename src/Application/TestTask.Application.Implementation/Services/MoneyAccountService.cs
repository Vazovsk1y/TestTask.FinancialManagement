using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class MoneyAccountService : IMoneyAccountService
{
	public Task<Result<MoneyAccountId>> CreateAsync(UserId requesterId, CurrencyId currencyId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<IReadOnlyCollection<MoneyAccountDTO>>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<MoneyAccountDTO>> GetByIdAsync(UserId requesterId, MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}