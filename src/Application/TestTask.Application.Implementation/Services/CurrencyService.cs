using TestTask.Application.Contracts;
using TestTask.Application.Services;

namespace TestTask.Application.Implementation.Services;

internal class CurrencyService : ICurrencyService
{
	public Task<IReadOnlyCollection<CurrencyDTO>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}