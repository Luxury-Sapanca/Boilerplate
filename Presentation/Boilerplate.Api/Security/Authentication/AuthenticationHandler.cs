namespace Boilerplate.Api.Security.Authentication;

public sealed class AuthenticationHandler : AuthenticationHandler<AuthenticationConfiguration>
{
    public AuthenticationHandler(
        IOptionsMonitor<AuthenticationConfiguration> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out var headerValue))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!headerValue.Scheme.Equals(JwtBearerDefaults.AuthenticationScheme, StringComparison.InvariantCultureIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken;

        try
        {
            if (tokenHandler.ReadToken(headerValue.Parameter) is JwtSecurityToken token)
            {
                jwtToken = token;
            }
            else
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }
        }
        catch (ArgumentException)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!Options.ClaimsIssuer!.Equals(jwtToken.Issuer))
        {
            return Task.FromResult(AuthenticateResult.Fail($"Expected '{Options.ClaimsIssuer}' issuer, but got '{jwtToken.Issuer}'."));
        }

        if (!Options.Subject.Equals(jwtToken.Subject))
        {
            return Task.FromResult(AuthenticateResult.Fail($"Expected '{Options.Subject}' subject, but got '{jwtToken.Subject}'."));
        }

        if (!Options.ClientSecret.Equals(headerValue.Parameter))
        {
            return Task.FromResult(AuthenticateResult.Fail("Unauthenticated."));
        }

        var claims = jwtToken.Claims;
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}