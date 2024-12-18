using Application;
using Application.Util;
using Data;
using Data.Repo.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("PGConnectionString") ?? throw new Exception("Connectionstring not found"); 

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://frontend:4200") // URL of your frontend container
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Allows cookies (if needed)
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEncryptionUtil, EncryptionUtil>();

DataDependencies.AddDependencies(builder.Services, connectionstring);
ApplicationDependencies.AddDependencies(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context =  scope.ServiceProvider.GetService<IDbContextFactory<Context>>().CreateDbContext();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

// Add CORS middleware
app.UseCors("AllowFrontend");  // Apply the "AllowFrontend" CORS policy here

app.UseAuthorization();

app.MapControllers();

app.Run();
