using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Railway.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Railway.HttpClients.Registrars;

/// <summary>
/// Registers the Railway GraphQL HttpClient wrapper for dependency injection.
/// </summary>
public static class RailwayGraphQlHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="RailwayGraphQlHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddRailwayGraphQlHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IRailwayGraphQlHttpClient, RailwayGraphQlHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="RailwayGraphQlHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddRailwayGraphQlHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IRailwayGraphQlHttpClient, RailwayGraphQlHttpClient>();

        return services;
    }
}

