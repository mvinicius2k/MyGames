using System.Diagnostics;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Shared;

namespace Api;

public class WeatherFunctions
{
    private readonly ILogger _logger;

        public WeatherFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<WeatherFunctions>();
        }

        [Function("Weather-Get")]
        [OpenApiOperation("Weather-Get", "Weather-Get", Description = "Obtém a previsão do tempo")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, MimeMapping.KnownMimeTypes.Json, typeof(WeatherForecast[]))]
        public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            Debugger.Log(0, "Info", "Olá");
            _logger.LogInformation("Função do tempo invocada");

            var model = new WeatherForecast[]
            {
                new WeatherForecast{
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Summary = "Resumo 1",
                    TemperatureC = 32
                },
                new WeatherForecast{
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    Summary = "Resumo 5",
                    TemperatureC = 24
                },
                new WeatherForecast{
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                    Summary = "Resumo 4",
                    TemperatureC = 30
                },
                new WeatherForecast{
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                    Summary = "Resumo 9",
                    TemperatureC = 11
                },
                new WeatherForecast{
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(41)),
                    Summary = "Resumo 10",
                    TemperatureC = 3
                },
            };

            var response = req.CreateResponse();
            await response.WriteAsJsonAsync(model);


            return response;
        }
}
