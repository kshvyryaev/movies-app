using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityModel;
using IdentityServer.Options;
using Newtonsoft.Json;

namespace IdentityServer
{
    public static class IdentityConfiguration
    {
        public static List<Client> GetClients(IdentityOptions options)
        {
            return new()
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
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
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
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        options.MoviesApiScope.ScopeName,
                        "roles"
                    }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string> { "role" })
            };
        }

        public static List<ApiResource> GetApiResources()
        {
            return new()
            {
            };
        }

        public static List<ApiScope> GetApiScopes(IdentityOptions options)
        {
            return new()
            {
                new ApiScope(options.MoviesApiScope.ScopeName, options.MoviesApiScope.ScopeDisplayName)
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

            return new()
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
                },
                new TestUser
                {
                    SubjectId = "818727",
                    Username = "alice",
                    Password = "alice",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, JsonConvert.SerializeObject(address), IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim(JwtClaimTypes.Role, "user")
                    }
                },
                new TestUser
                {
                    SubjectId = "88421113",
                    Username = "bob",
                    Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, JsonConvert.SerializeObject(address), IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}