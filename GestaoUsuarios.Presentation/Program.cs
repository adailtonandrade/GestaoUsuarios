using Blazored.SessionStorage;
using GestaoUsuarios.Presentation;
using GestaoUsuarios.Presentation.Handlers;
using GestaoUsuarios.Presentation.Repository;
using GestaoUsuarios.Presentation.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<AuthenticationHandler>();

builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddBlazoredSessionStorageAsSingleton();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
