using CheckPromise.Ingestion.Sources.Nbu;
using Microsoft.Extensions.DependencyInjection;

namespace CheckPromise.Ingestion;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIndicatorIngestion(this IServiceCollection services)
    {
        services.AddScoped<IIndicatorIngestionService, IndicatorIngestionService>();

        services.AddHttpClient<NbuUsdExchangeRateSource>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("checkpromise-ingester/1.0");
        });
        services.AddScoped<IIndicatorDataSource>(sp => sp.GetRequiredService<NbuUsdExchangeRateSource>());

        return services;
    }
}
