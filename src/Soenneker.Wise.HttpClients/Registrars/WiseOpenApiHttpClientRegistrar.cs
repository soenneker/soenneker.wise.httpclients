using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Wise.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Wise.HttpClients.Registrars;

/// <summary>
/// Registers the Wise API HTTP client provider.
/// </summary>
public static class WiseOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds the Wise HTTP client provider as a singleton service.
    /// </summary>
    public static IServiceCollection AddWiseOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IWiseOpenApiHttpClient, WiseOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds the Wise HTTP client provider as a scoped service.
    /// </summary>
    public static IServiceCollection AddWiseOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IWiseOpenApiHttpClient, WiseOpenApiHttpClient>();

        return services;
    }
}
