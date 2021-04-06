using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MoviesApp.Api.Options
{
    public class AuthenticationOptions
    {
        public const string SectionKey = "Authentication";
        
        public JwtBearerOptions JwtBearer { get; set; }
    }
}