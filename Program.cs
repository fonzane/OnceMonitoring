using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebServerExample.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Load database configuration from appsettings.json
var databaseSettings = builder.Configuration.GetSection("DatabaseSettings");
var connectionString = databaseSettings.GetValue<string>("ConnectionString");
var databaseName = databaseSettings.GetValue<string>("DatabaseName");

// Register DatabaseConfig as a singleton
builder.Services.AddSingleton(new DatabaseConfig(connectionString, databaseName));

var app = builder.Build();

// Configure middleware and endpoints
app.UseRouting();
app.MapControllers();

app.Run();
