namespace Boilerplate.Domain.Configurations;

public class AuthenticationConfiguration : AuthenticationSchemeOptions
{
    public const string AuthenticationScheme = "Boilerplate";

    public string Subject { get; set; }

    public string ClientSecret { get; set; }
}