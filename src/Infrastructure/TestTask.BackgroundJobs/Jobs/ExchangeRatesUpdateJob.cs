using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.Application.Services;
using TestTask.DAL;

namespace TestTask.BackgroundJobs.Jobs;

internal class ExchangeRatesUpdateJob(
    TestTaskDbContext dbContext, 
    ILogger<ExchangeRatesUpdateJob> logger,
    IClock clock)
{
    private readonly TestTaskDbContext _dbContext = dbContext;
    private readonly ILogger _logger = logger;
    private readonly IClock _clock = clock;

    public async Task UpdateExchangeRatesAsync()
    {
        var date = _clock.GetUtcNow();
        _logger.LogInformation("{jobName} started at {UtcNow}.", nameof(ExchangeRatesUpdateJob), date);

        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            int updatedRatesCount = await _dbContext
                .ExchangeRates
                .ExecuteUpdateAsync(setters => setters
                          .SetProperty(i => i.Value, i => (decimal)(Random.Shared.NextDouble() * (10.0 - 0.1) + 0.1))
                          .SetProperty(i => i.UpdatedAt, date));

            transaction.Commit();

            _logger.LogInformation("{updatedRatesCount} rates updated. Transaction commited.", updatedRatesCount);
        }
        catch (Exception ex) 
        {
            transaction.Rollback();
            _logger.LogError(ex, "Something went wrong. Transaction rollbacked.");
            throw;
        }
    }
}
