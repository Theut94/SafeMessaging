using Application;
using BlazorApp;
using Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var connectionstring = builder.Configuration.GetConnectionString("PGConnectionString") ?? throw new NullReferenceException("Connectionstring not found.");

//Dependencies
builder.Services.AddScoped<WebApp.FrontEndEncryption.IEncryptionUtil, WebApp.FrontEndEncryption.EncryptionUtil>();
DataDependencies.AddDependencies(builder.Services, connectionstring);
ApplicationDependencies.AddDependencies(builder.Services);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
