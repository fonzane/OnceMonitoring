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
var collectionName = databaseSettings.GetValue<string>("ConnectionName");
const long maxByteSize = 10L * 1024 * 1024 * 1024; // 10 GB in bytes

// Register DatabaseConfig as a singleton
if (connectionString == null || databaseName == null || collectionName == null)
{
  Console.WriteLine(connectionString, databaseName, collectionName);
  Console.WriteLine("Failed to load database config. Aborting...");
  return;
}
builder.Services.AddSingleton(new DatabaseConfig(connectionString, databaseName, collectionName ,maxByteSize));

var app = builder.Build();

// Configure middleware and endpoints
app.UseRouting();
app.MapControllers();

app.Run();
