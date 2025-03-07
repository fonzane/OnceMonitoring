using Microsoft.AspNetCore.Authentication;
using OnceMonitoring.Config;
using OnceMonitoring.Auth;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Nur auf Windows den Windows-Dienstmodus aktivieren
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    builder.Host.UseWindowsService();
}

// Bind the Authentication section from appsettings.json to AuthenticationConfig
builder.Services.Configure<AuthenticationConfig>(builder.Configuration.GetSection("Authentication"));

// Register services
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Optional: Logging-Provider anpassen, um EventLog zu vermeiden
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Load database configuration from appsettings.json
var databaseSettings = builder.Configuration.GetSection("DatabaseSettings");
var connectionString = databaseSettings.GetValue<string>("ConnectionString");
var databaseName = databaseSettings.GetValue<string>("DatabaseName");
var collectionName = databaseSettings.GetValue<string>("ConnectionName");
const long maxByteSize = 10L * 1024 * 1024 * 1024; // 10 GB in bytes

// Register DatabaseConfig as a singleton
if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(collectionName))
{
    Console.WriteLine("Failed to load database config. Aborting...");
    return;
}
builder.Services.AddSingleton(new DatabaseConfig(connectionString, databaseName, collectionName, maxByteSize));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var urls = builder.Configuration.GetSection("Kestrel:Endpoints:Http:Url").Value ?? "http://0.0.0.0:5001";
app.Urls.Add(urls);

app.Run();
