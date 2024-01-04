using TestTask.Application.Contracts;

namespace TestTask.Application.Services;

public interface ICurrencyService
{
	Task<IReadOnlyCollection<CurrencyDTO>> GetAllAsync(CancellationToken cancellationToken = default);
}