using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.BackgroundJobs.Jobs;

namespace TestTask.BackgroundJobs;

public static class Registrator
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
    {
        string hangfireDatabaseConnectionString = configuration.GetConnectionString("HangfireConnectionString") ??
            throw new InvalidOperationException("Hangfire connection string is not defined.");

        services.AddScoped<ExchangeRatesUpdateJob>();
        services.AddHangfire((_, config) => 
        {
            config
            .UseSqlServerStorage(hangfireDatabaseConnectionString, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true
            })
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings();


            RecurringJob.AddOrUpdate<ExchangeRatesUpdateJob>(nameof(ExchangeRatesUpdateJob), e => e.UpdateExchangeRatesAsync(), Cron.Daily);
        });
        services.AddHangfireServer();

        return services;
    }
}
