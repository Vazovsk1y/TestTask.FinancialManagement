using TestTask.Application.Common;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IExchangeRateProvider
{
	Result<decimal> GetRate(CurrencyId FromId, CurrencyId ToId);
}