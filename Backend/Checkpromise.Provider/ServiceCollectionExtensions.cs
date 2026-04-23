using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkpromise.Provider;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFtpClientDataProvider(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<FtpClientDataProviderOptions>()
            .Bind(configuration.GetSection(FtpClientDataProviderOptions.SectionName));

        services.AddScoped<IClientDataProvider, FtpClientDataProvider>();

        return services;
    }
}
