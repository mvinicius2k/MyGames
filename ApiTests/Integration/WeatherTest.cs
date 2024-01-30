using DotNet.Testcontainers.Builders;
using Ductus.FluentDocker.Services;


namespace ApiTests.Integration;

public class WeatherTest
{
    


    [Fact]
    public async Task Get_ForecastWeather_Returns_SucessCode()
    {

        var http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000/api/")
        };

        HttpResponseMessage response = await http.GetAsync("Weather-Get");

        Assert.True(response.IsSuccessStatusCode);
    }
}
