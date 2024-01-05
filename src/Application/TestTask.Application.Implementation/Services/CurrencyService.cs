using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.DAL;

namespace TestTask.Application.Implementation.Services;

internal class CurrencyService(TestTaskDbContext dbContext) : ICurrencyService
{
	public async Task<Result<IReadOnlyCollection<CurrencyDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var result = await dbContext.Currencies.Select(e => e.ToDTO()).ToListAsync(cancellationToken);
		return result;
	}
}