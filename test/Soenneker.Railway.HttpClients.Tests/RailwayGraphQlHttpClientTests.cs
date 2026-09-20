using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Railway.HttpClients.Abstract;
using Soenneker.Railway.HttpClients.Registrars;

namespace Soenneker.Railway.HttpClients.Tests;

public sealed class RailwayGraphQlHttpClientTests
{
    [Test]
    public async Task ConfiguresAndCachesBearerClient()
    {
        await using var provider = CreateProvider(new() { ["Railway:ApiKey"] = "test-token" });
        var factory = provider.GetRequiredService<IRailwayGraphQlHttpClient>();
        var client = await factory.Get();
        if (!ReferenceEquals(client, await factory.Get()) || client.BaseAddress?.AbsoluteUri != "https://backboard.railway.com/graphql/v2" ||
            client.DefaultRequestHeaders.Authorization?.ToString() != "Bearer test-token")
            throw new Exception("Bearer authentication, endpoint or singleton caching is incorrect.");
    }

    [Test]
    public async Task SupportsProjectTokensAndCustomEndpoint()
    {
        await using var provider = CreateProvider(new()
        {
            ["Railway:ApiKey"] = "project-token", ["Railway:AuthHeaderName"] = "Project-Access-Token",
            ["Railway:AuthHeaderValueTemplate"] = "{token}", ["Railway:ClientBaseUrl"] = "https://example.com/graphql"
        });
        var client = await provider.GetRequiredService<IRailwayGraphQlHttpClient>().Get();
        if (client.DefaultRequestHeaders.GetValues("Project-Access-Token").Single() != "project-token" ||
            client.DefaultRequestHeaders.Authorization != null || client.BaseAddress?.AbsoluteUri != "https://example.com/graphql")
            throw new Exception("Project token configuration is incorrect.");
    }

    private static ServiceProvider CreateProvider(Dictionary<string, string?> settings) => new ServiceCollection()
        .AddLogging().AddSingleton<IConfiguration>(new ConfigurationBuilder().AddInMemoryCollection(settings).Build())
        .AddRailwayGraphQlHttpClientAsSingleton().BuildServiceProvider();
}
