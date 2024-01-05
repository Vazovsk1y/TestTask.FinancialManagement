using TestTask.Application.Common;
using TestTask.Application.Contracts;

namespace TestTask.Application.Services;

public interface ICurrencyService
{
	Task<Result<IReadOnlyCollection<CurrencyDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
}