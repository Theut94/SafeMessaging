using Application;
using Application.Util;
using Data;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionstring = builder.Configuration.GetConnectionString("PGConnectionString") ?? throw new NullReferenceException("Connectionstring not found.");

//Dependencies
builder.Services.AddScoped<WebApp.FrontEndEncryption.IEncryptionUtil, WebApp.FrontEndEncryption.EncryptionUtil>();
DataDependencies.AddDependencies(builder.Services, connectionstring);
ApplicationDependencies.AddDependencies(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
