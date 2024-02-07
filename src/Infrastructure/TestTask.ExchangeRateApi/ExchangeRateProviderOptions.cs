namespace TestTask.ExchangeRateApi;

public class ExchangeRateProviderOptions
{
    public const string SectionName = nameof(ExchangeRateProviderOptions);

    public required string BaseAddress { get; init; }

    public required string ApiKey { get; init; }
}