using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace MoviesApp.Client.Options
{
    public class AuthenticationOptions
    {
        public const string SectionKey = "Authentication";

        public OpenIdConnectOptions OpenIdConnect { get; set; }
    }
}
