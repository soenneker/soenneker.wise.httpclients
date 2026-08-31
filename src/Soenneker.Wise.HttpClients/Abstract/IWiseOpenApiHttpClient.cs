using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Wise.HttpClients.Abstract;

/// <summary>
/// Provides a cached <see cref="HttpClient"/> configured for the Wise API.
/// </summary>
public interface IWiseOpenApiHttpClient : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the configured Wise API client.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The cached HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
