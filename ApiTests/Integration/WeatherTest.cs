using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests.Integration;

public class WeatherTest : IClassFixture<AzureFunctionsTestcontainersFixture>
{
    private readonly AzureFunctionsTestcontainersFixture _containerFixture;

    public WeatherTest(AzureFunctionsTestcontainersFixture containerFixture)
    {
        _containerFixture = containerFixture;
    }

    [Fact]
    public async Task Get_ForecastWeather_Returns_SucessCode()
    {
        var http = new HttpClient();

        var requestUri = new UriBuilder(
            Uri.UriSchemeHttp,
            _containerFixture.AzureFunctionsContainerInstance.Hostname,
            _containerFixture.AzureFunctionsContainerInstance.GetMappedPublicPort(80),
            "Weather-Get"
        ).Uri;

        HttpResponseMessage response = await http.GetAsync(requestUri);

        Assert.True(response.IsSuccessStatusCode);
    }
}
