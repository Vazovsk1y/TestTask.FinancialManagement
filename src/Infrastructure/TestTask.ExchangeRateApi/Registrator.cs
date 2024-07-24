using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestTask.Application.Services;


namespace TestTask.ExchangeRateApi;

public static class Registrator
{
    public static IServiceCollection AddExchangeRateProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExchangeRateProviderOptions>(configuration.GetSection(ExchangeRateProviderOptions.SectionName));

        services.AddMemoryCache();

        services.AddHttpClient<IExchangeRateProvider, ExchangeRateProvider>((serviceProvider, client) =>
        {
            // stores in secrets.json
            var options = serviceProvider.GetRequiredService<IOptions<ExchangeRateProviderOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseAddress);
        });

        return services;
    }
}
