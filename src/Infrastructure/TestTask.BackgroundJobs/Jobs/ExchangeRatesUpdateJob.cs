using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Entities;

namespace TestTask.BackgroundJobs.Jobs;

internal class ExchangeRatesUpdateJob(
    TestTaskDbContext dbContext,
    ILogger<ExchangeRatesUpdateJob> logger,
    IClock clock,
    IExchangeRateProvider exchangeRateProvider)
{
    private readonly TestTaskDbContext _dbContext = dbContext;
    private readonly ILogger _logger = logger;
    private readonly IClock _clock = clock;
    private readonly IExchangeRateProvider _exchangeRateProvider = exchangeRateProvider;

    public async Task UpdateExchangeRatesAsync()
    {
        var date = _clock.GetUtcNow();
        _logger.LogInformation("{jobName} started at {UtcNow}.", nameof(ExchangeRatesUpdateJob), date);

        var currencies = await _dbContext
            .Currencies
            .AsNoTracking()
            .Select(e => e.Id)
            .ToListAsync();

        var ratesResults = await Task.WhenAll(currencies.Select(e => _exchangeRateProvider.GetRatesAsync(e)));
        var failedResults = ratesResults.Where(e => e.IsFailure);
        if (failedResults.Any())
        {
            throw new Exception($"Failed results detected. \r\n{string.Join(Environment.NewLine, failedResults.Select(e => e.ErrorMessage))}");
        }

        var rates = ratesResults
            .SelectMany(e => e.Value.Rates, (response, rate) => new { Response = response.Value, Rate = rate })
            .ToDictionary(pair => new Key(pair.Response.BaseCurrencyId, pair.Rate.Key), pair => pair.Rate.Value);

        foreach (var rate in _dbContext.ExchangeRates)
        {
            rate.Value = rates[new Key(rate.CurrencyFromId, rate.CurrencyToId)];
            rate.UpdatedAt = date;
        }

        int updatedRatesCount = await _dbContext.SaveChangesAsync();
        _logger.LogInformation("{updatedRatesCount} rates updated.", updatedRatesCount);
    }

    private record Key(CurrencyId From, CurrencyId To);
}
