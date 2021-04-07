using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer.Options;
using System.Text.Json;

namespace IdentityServer
{
    public static class IdentityConfiguration
    {
        public static List<Client> GetClients(IdentityOptions options) =>
            new List<Client>
            {
                new Client
                {
                    ClientId = options.MoviesClientId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(options.MoviesClientSecret.Sha256())
                    },
                    AllowedScopes = { options.MoviesApiScopeName }
                }
            };

        public static List<IdentityResource> GetIdentityResources(IdentityOptions options) =>
            new List<IdentityResource>
            {
                
            };
        
        public static List<ApiResource> GetApiResources(IdentityOptions options) =>
            new List<ApiResource>
            {
                
            };
        
        public static List<ApiScope> GetApiScopes(IdentityOptions options) =>
            new List<ApiScope>
            {
                new ApiScope(options.MoviesApiScopeName, options.MoviesApiScopeDisplayName)
            };

        public static List<TestUser> GetTestUsers()
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };

            var testUsers = new List<TestUser>
            {
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
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
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
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                    }
                }
            };

            return testUsers;
        }
    }
}