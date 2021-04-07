namespace IdentityServer.Options
{
    public class IdentityOptions
    {
        public const string SectionKey = "Identity";
        
        public string MoviesClientId { get; set; }

        public string MoviesClientSecret { get; set; }

        public string MoviesApiScopeName { get; set; }
        
        public string MoviesApiScopeDisplayName { get; set; }
    }
}