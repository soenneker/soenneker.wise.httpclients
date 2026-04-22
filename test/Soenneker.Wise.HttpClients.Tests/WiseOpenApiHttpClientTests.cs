using Soenneker.Wise.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Wise.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class WiseOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IWiseOpenApiHttpClient _httpclient;

    public WiseOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IWiseOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
