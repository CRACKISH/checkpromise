using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckPromise.BusinessLayer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClientDataBuilder(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<ClientDataBuilderOptions>()
            .Bind(configuration.GetSection(ClientDataBuilderOptions.SectionName));

        services.AddScoped<IClientDataBuilder, ClientDataBuilder>();

        return services;
    }
}
