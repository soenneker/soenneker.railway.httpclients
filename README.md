# Soenneker.Railway.HttpClients

A cached HTTP client for Railway's GraphQL API on .NET 10.

Register with `services.AddRailwayGraphQlHttpClientAsSingleton()` and resolve `IRailwayGraphQlHttpClient`. Call `Get(cancellationToken)` to obtain the shared client.

Configuration:

| Key | Value/default |
| --- | --- |
| `Railway:ApiKey` | Required account, workspace or project token |
| `Railway:ClientBaseUrl` | `https://backboard.railway.com/graphql/v2` |
| `Railway:AuthHeaderName` | `Authorization` |
| `Railway:AuthHeaderValueTemplate` | `Bearer {token}` |

Account/workspace tokens use the defaults. Project tokens require `AuthHeaderName=Project-Access-Token` and `AuthHeaderValueTemplate={token}`. Environment variables use double underscores, for example `Railway__ApiKey`. Keep tokens in your configuration secret store.

The service owns the cached HTTP client. Callers should not dispose the client returned by `Get`. Disposing the service removes its cache entry. The configured endpoint is the full GraphQL URL, not a server root.
