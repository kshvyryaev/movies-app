using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityModel;
using IdentityServer.Options;

namespace IdentityServer
{
    public static class IdentityConfiguration
    {
        public static List<Client> GetClients(IdentityOptions options) => new()
        {
            new Client
            {
                ClientName = options.MoviesApiClient.ClientName,
                ClientId = options.MoviesApiClient.ClientId,
                ClientSecrets =
                {
                    new Secret(options.MoviesApiClient.ClientSecret.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    options.MoviesApiScope.ScopeName
                }
            },
            new Client
            {
                ClientName = options.MoviesMvcClient.ClientName,
                ClientId = options.MoviesMvcClient.ClientId,
                ClientSecrets =
                {
                    new Secret(options.MoviesMvcClient.ClientSecret.Sha256())
                },
                AllowedGrantTypes = GrantTypes.Code,
                AllowRememberConsent = false,
                RedirectUris =
                {
                    options.MoviesMvcClient.RedirectUri
                },
                PostLogoutRedirectUris =
                {
                    options.MoviesMvcClient.PostLogoutRedirectUri
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static List<IdentityResource> GetIdentityResources() => new()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        
        public static List<ApiResource> GetApiResources() => new()
        {
                
        };
        
        public static List<ApiScope> GetApiScopes(IdentityOptions options) => new()
        {
            new ApiScope(options.MoviesApiScope.ScopeName, options.MoviesApiScope.ScopeDisplayName)
        };

        public static List<TestUser> GetTestUsers() => new()
        {
            new TestUser
            {
                SubjectId = "20266D24-AADE-4D7A-8202-20FC93653D29",
                Username = "kshv",
                Password = "kshv",
                Claims =
                {
                    new Claim(JwtClaimTypes.GivenName, "Konstantin"),
                    new Claim(JwtClaimTypes.FamilyName, "Shvyryaev")
                }
            }
        };
    }
}