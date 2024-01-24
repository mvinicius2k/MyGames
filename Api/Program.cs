using System.Diagnostics;
using Api;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


const bool Restart = true;

#if DEBUG

while (!Debugger.IsAttached)
    Thread.Sleep(1000);

#endif

var builder = new HostBuilder();


builder.ConfigureFunctionsWorkerDefaults(w =>
{
    w.UseNewtonsoftJson();

})
.AddGraphQLFunction(g => g.AddQueryType<Query>())
.ConfigureServices((hostContext, services) =>
{
    services.AddApplicationInsightsTelemetryWorkerService();
    services.ConfigureFunctionsApplicationInsights();

    services.AddDbContext<Context>(opt =>
    {
        var connectionString = hostContext.Configuration.GetSection(Values.ConnectionKey).Value ?? throw new Exception("String de conexão inv�lida");
        var version = new MariaDbServerVersion(new Version("10.6"));
        opt.UseMySql(connectionString, version);

    });
    services.AddScoped<DbInit>();
});

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbInit = services.GetRequiredService<DbInit>();
    dbInit.Prepare(restart: Restart);
    dbInit.Seed();

}

host.Run();
