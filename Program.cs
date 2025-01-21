using Microsoft.AspNetCore.Authentication;
using OnceMonitoring.Config;
using OnceMonitoring.Auth;

var builder = WebApplication.CreateBuilder(args);

// Bind the Authentication section from appsettings.json to AuthenticationConfig
builder.Services.Configure<AuthenticationConfig>(builder.Configuration.GetSection("Authentication"));

// Register services
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

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

app.UseRouting();
// Ensure the authentication and authorization middleware are used correctly
app.UseAuthentication();  // This should be before UseAuthorization
app.UseAuthorization();   // Ensure authorization is checked after authentication

// Configure middleware and endpoints
app.MapControllers();

app.Run();
