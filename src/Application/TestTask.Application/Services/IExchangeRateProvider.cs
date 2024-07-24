using TestTask.Application.Shared;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IExchangeRateProvider
{
	Task<Result<ExchangeRateResponse>> GetRatesAsync(CurrencyId baseCurrencyId, CancellationToken cancellationToken = default);
}

public record ExchangeRateResponse(CurrencyId BaseCurrencyId, IReadOnlyDictionary<CurrencyId, decimal> Rates);