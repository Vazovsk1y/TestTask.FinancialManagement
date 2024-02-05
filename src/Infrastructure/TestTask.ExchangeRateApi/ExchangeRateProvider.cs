using Microsoft.EntityFrameworkCore;
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
    TestTaskDbContext dbContext) : IExchangeRateProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ExchangeRateProviderOptions _options = options.Value;
    private readonly TestTaskDbContext _dbContext = dbContext;

    public async Task<Result<IReadOnlyDictionary<CurrencyId, decimal>>> GetRatesAsync(CurrencyId baseCurrencyId, CancellationToken cancellationToken = default)
    {
        var currency = await _dbContext
            .Currencies
            .SingleOrDefaultAsync(e => e.Id == baseCurrencyId, cancellationToken);

        if (currency is null)
        {
            return Result.Failure<IReadOnlyDictionary<CurrencyId, decimal>>($"Currency with passed id is not exists.");
        }

        string baseCurencyCode = currency.AlphabeticCode;
        string url = $"{_options.ApiKey}/latest/{baseCurencyCode}";

        var response = await _httpClient.GetFromJsonAsync<ExchangeRateApiResponse>(url, cancellationToken)
            ?? throw new Exception("Exchange rate api response was equal to null.");

        var currencies = await _dbContext
            .Currencies
            .AsNoTracking()
            .Where(e => response.ConversionRates.Keys.Contains(e.AlphabeticCode) && e.Id != currency.Id)
            .Select(e => new { e.Id, e.AlphabeticCode })
            .ToListAsync(cancellationToken);

        var result = currencies.ToDictionary(k => k.Id, v => response.ConversionRates[v.AlphabeticCode]);
        return result;
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

