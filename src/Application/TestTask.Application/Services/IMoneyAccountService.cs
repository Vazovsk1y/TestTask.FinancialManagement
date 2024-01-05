using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IMoneyAccountService
{
	Task<Result<IReadOnlyCollection<MoneyAccountDTO>>> GetAllByUserIdAsync(UserId requesterId, CancellationToken cancellationToken = default);

	Task<Result<MoneyAccountDTO>> GetByIdAsync(UserId requesterId, MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default);

	Task<Result<MoneyAccountId>> CreateAsync(UserId requesterId, CurrencyId currencyId, CancellationToken cancellationToken = default);
}