using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp 
    => new HttpClient 
    { 
        BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? throw new Exception("Api não encontrada")) 
    });

await builder.Build().RunAsync();
