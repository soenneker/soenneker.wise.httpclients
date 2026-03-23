using Soenneker.Wise.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Wise.HttpClients.Tests;

[Collection("Collection")]
public sealed class WiseOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IWiseOpenApiHttpClient _httpclient;

    public WiseOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IWiseOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
