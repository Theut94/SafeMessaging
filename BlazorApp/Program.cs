using Application;
using BlazorApp;
using Data;
using Data.Repo.Context;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//var connectionstring = builder.Configuration.GetConnectionString("PGConnectionString") ?? throw new NullReferenceException("Connectionstring not found.");
var connectionstring = "Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=postgres;";

//Dependencies
builder.Services.AddScoped<WebApp.FrontEndEncryption.IDiffieHellmanUtil, WebApp.FrontEndEncryption.DiffieHellmanUtil>();
builder.Services.AddScoped<WebApp.FrontEndEncryption.IEncryptionUtil, WebApp.FrontEndEncryption.EncryptionUtil>();
DataDependencies.AddDependencies(builder.Services, connectionstring);
ApplicationDependencies.AddDependencies(builder.Services);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<Context>>();
    using (var context = contextFactory.CreateDbContext())
    {
        context.Database.EnsureCreated();  // Ensures the database is created if it doesn't exist
    }
}
await app.RunAsync();
