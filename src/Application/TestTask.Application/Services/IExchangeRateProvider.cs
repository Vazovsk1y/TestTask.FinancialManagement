using TestTask.Application.Common;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IExchangeRateProvider
{
	Task<Result<IReadOnlyDictionary<CurrencyId, decimal>>> GetRatesAsync(CurrencyId baseCurrencyId, CancellationToken cancellationToken = default);
}
