using TestTask.Application.Common;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

public class RandomExchangeRateProvider : IExchangeRateProvider
{
    public Task<Result<IReadOnlyDictionary<CurrencyId, decimal>>> GetRatesAsync(CurrencyId baseCurrency, CancellationToken cancellationToken = default)
    {
        // TODO
        // Implement receiving rate value from another external api.

        throw new NotImplementedException();
    }
}