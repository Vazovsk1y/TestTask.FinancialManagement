using TestTask.Application.Common;
using TestTask.Application.Services;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

public class RandomExchangeRateProvider : IExchangeRateProvider
{
	public Result<decimal> GetRate(CurrencyId FromId, CurrencyId ToId)
	{
		// TODO
		// Implement receiving rate value from another external api.

		double randomValue = (Random.Shared.NextDouble() * (10.0 - 0.1) + 0.1);
		return (decimal)Math.Round(randomValue, 4);
	}
}