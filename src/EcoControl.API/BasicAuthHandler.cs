using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _config;

    public BasicAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder,
        IConfiguration config) : base(options, logger, encoder)
    {
        _config = config;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return Task.FromResult(AuthenticateResult.Fail("Sem header Authorization."));
        try
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]!);
            var credBytes = Convert.FromBase64String(header.Parameter!);
            var creds = Encoding.UTF8.GetString(credBytes).Split(':');
            var username = creds[0];
            var password = creds[1];

            var adminUser = _config["BasicAuth:AdminUser"];
            var adminPass = _config["BasicAuth:AdminPassword"];
            var viewerUser = _config["BasicAuth:ViewerUser"];
            var viewerPass = _config["BasicAuth:ViewerPassword"];

            string? role = null;
            if (username == adminUser && password == adminPass) role = "admin";
            else if (username == viewerUser && password == viewerPass) role = "viewer";
            else return Task.FromResult(AuthenticateResult.Fail("Credenciais inválidas."));

            var claims = new[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, role) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Header inválido."));
        }
    }
}