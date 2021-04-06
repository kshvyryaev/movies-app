using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

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
        
        public static List<TestUser> GetTestUsers() =>
            new List<TestUser>
            {
                
            };
    }
}