using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IMoneyAccountService
{
	Task<Result<IReadOnlyCollection<MoneyAccountDTO>>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);

	Task<Result<MoneyAccountDTO>> GetByIdAsync(MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default);

	Task<Result<MoneyAccountId>> CreateAsync(CurrencyId currencyId, CancellationToken cancellationToken = default);
}