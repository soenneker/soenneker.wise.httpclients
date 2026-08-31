[![](https://img.shields.io/nuget/v/soenneker.wise.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.wise.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.wise.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.wise.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.wise.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.wise.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.wise.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.wise.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Wise.HttpClients

Provides a cached `HttpClient` configured with a Wise API base address and bearer access token.

## Installation

```bash
dotnet add package Soenneker.Wise.HttpClients
```

## Configuration

```json
{
  "Wise": {
    "AccessToken": "your-access-token",
    "ClientBaseUrl": "https://api.wise.com/"
  }
}
```

For sandbox calls, use `https://api.wise-sandbox.com/`. The package sends the token you provide; it does not obtain or refresh OAuth tokens. `Wise:ApiKey` remains supported as a legacy alias for `AccessToken`.

## Registration and usage

```csharp
using Soenneker.Wise.HttpClients.Abstract;
using Soenneker.Wise.HttpClients.Registrars;

services.AddWiseOpenApiHttpClientAsSingleton();

public sealed class WiseProfileService
{
    private readonly IWiseOpenApiHttpClient _clientProvider;

    public WiseProfileService(IWiseOpenApiHttpClient clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async Task<string> ListProfiles(CancellationToken cancellationToken)
    {
        HttpClient client = await _clientProvider.Get(cancellationToken);
        return await client.GetStringAsync("v2/profiles", cancellationToken);
    }
}
```

Use `AddWiseOpenApiHttpClientAsScoped()` when the provider should follow a scope. Each provider owns its cached client and removes it when disposed.
