using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TestTask.Application.Common;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Entities;

namespace TestTask.ExchangeRateApi;

internal class ExchangeRateProvider(
    IOptions<ExchangeRateProviderOptions> options,
    HttpClient httpClient,
    IServiceScopeFactory serviceScopeFactory,
    IMemoryCache cache) : IExchangeRateProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ExchangeRateProviderOptions _options = options.Value;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly IMemoryCache _cache = cache;
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(6);

    public async Task<Result<ExchangeRateResponse>> GetRatesAsync(CurrencyId baseCurrencyId, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var baseCurrency = await dbContext
            .Currencies
            .SingleOrDefaultAsync(e => e.Id == baseCurrencyId, cancellationToken);

        if (baseCurrency is null)
        {
            return Result.Failure<ExchangeRateResponse>($"Currency with passed id is not exists.");
        }

        string baseCurencyCode = baseCurrency.AlphabeticCode;
        string url = $"{_options.ApiKey}/latest/{baseCurencyCode}";

        ExchangeRateApiResponse? apiResponse;
        if (_cache.TryGetValue<ExchangeRateApiResponse>(baseCurencyCode, out var responseFromCache))
        {
            apiResponse = responseFromCache;
        }
        else
        {
            apiResponse = await _httpClient.GetFromJsonAsync<ExchangeRateApiResponse>(url, cancellationToken) ?? throw new Exception("Exchange rate api response was equal to null.");
            _cache.Set(baseCurencyCode, apiResponse, CACHE_DURATION);
        }

        var currencies = await dbContext
            .Currencies
            .AsNoTracking()
            .Where(e => e.Id != baseCurrency.Id)
            .Select(e => new { e.Id, e.AlphabeticCode })
            .ToListAsync(cancellationToken);

        var result = currencies.ToDictionary(k => k.Id, v => apiResponse!.ConversionRates[v.AlphabeticCode]);
        return new ExchangeRateResponse(baseCurrencyId, result);
    }

    private record ExchangeRateApiResponse(
        [property: JsonPropertyName("result")] string Result,
        [property: JsonPropertyName("documentation")] string Documentation,
        [property: JsonPropertyName("terms_of_use")] string TermsOfUse,
        [property: JsonPropertyName("time_last_update_unix")] long TimeLastUpdateUnix,
        [property: JsonPropertyName("time_last_update_utc")] string TimeLastUpdateUtc,
        [property: JsonPropertyName("time_next_update_unix")] long TimeNextUpdateUnix,
        [property: JsonPropertyName("time_next_update_utc")] string TimeNextUpdateUtc,
        [property: JsonPropertyName("base_code")] string BaseCode,
        [property: JsonPropertyName("conversion_rates")] Dictionary<string, decimal> ConversionRates);
}

