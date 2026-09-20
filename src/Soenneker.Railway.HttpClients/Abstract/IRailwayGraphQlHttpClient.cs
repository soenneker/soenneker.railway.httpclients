using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Railway.HttpClients.Abstract;

/// <summary>
/// Provides a cached HTTP client for the Railway GraphQL API.
/// </summary>
public interface IRailwayGraphQlHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>Gets the cached client using Railway:ApiKey and the optional ClientBaseUrl, AuthHeaderName and AuthHeaderValueTemplate settings.</summary>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}


