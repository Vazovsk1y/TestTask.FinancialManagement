using TestTask.Application.Contracts;
using TestTask.Application.Shared;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface ICurrencyService
{
	Task<Result<IReadOnlyCollection<CurrencyDTO>>> GetAllAsync(CancellationToken cancellationToken = default);

	Task<Result<CurrencyId>> AddAsync(CurrencyAddDTO currencyAddDTO, CancellationToken cancellationToken = default);
}