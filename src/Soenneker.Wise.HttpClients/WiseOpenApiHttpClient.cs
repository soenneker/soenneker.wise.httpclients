using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Wise.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Wise.HttpClients;

///<inheritdoc cref="IWiseOpenApiHttpClient"/>
public sealed class WiseOpenApiHttpClient : IWiseOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _cacheKey = $"{nameof(WiseOpenApiHttpClient)}-{Guid.NewGuid():N}";

    private const string _prodBaseUrl = "https://api.wise.com/";

    public WiseOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_cacheKey, (config: _config, baseUrl: _config["Wise:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            string accessToken = state.config["Wise:AccessToken"] ?? state.config.GetValueStrict<string>("Wise:ApiKey");
            string authHeaderName = state.config["Wise:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["Wise:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", accessToken, StringComparison.Ordinal);

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_cacheKey);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_cacheKey);
    }
}
