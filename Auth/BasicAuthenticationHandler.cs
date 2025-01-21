using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OnceMonitoring.Auth
{
  public class AuthenticationConfig
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

  public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
  {
    private readonly AuthenticationConfig _authConfig;
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder,
        ISystemClock clock,
        IOptions<AuthenticationConfig> authConfig)
        : base(options, logger, encoder, clock)
    {
      _authConfig = authConfig.Value;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      if (!Request.Headers.ContainsKey("Authorization"))
      {
        return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
      }

      try
      {
        var authHeader = Request.Headers["Authorization"].ToString();

        // Ensure the header starts with "Basic "
        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
          return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }

        // Extract the base64 encoded credentials (after "Basic " prefix)
        var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();

        // Decode the Base64 credentials
        var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

        // Split the decoded string into username and password
        var credentials = decodedCredentials.Split(':');

        if (credentials.Length != 2)
        {
          return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header Format"));
        }

        var username = credentials[0];
        var password = credentials[1];

        if (username != _authConfig.Username || password != _authConfig.Password) {
          return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
        }

        // Here, no actual validation occurs; we're just accepting the header format
        var claims = new[] { new Claim(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
      }
      catch
      {
        return Task.FromResult(AuthenticateResult.Fail("Error Parsing Authorization Header"));
      }
    }
  }
}
