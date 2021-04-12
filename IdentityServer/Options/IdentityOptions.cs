namespace IdentityServer.Options
{
    public class IdentityOptions
    {
        public const string SectionKey = "Identity";

        public MoviesApiClientParams MoviesApiClient { get; set; }

        public MoviesMvcClientParams MoviesMvcClient { get; set; }

        public MoviesApiScopeParams MoviesApiScope { get; set; }
    }

    public class MoviesApiClientParams
    {
        public string ClientName { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

    public class MoviesMvcClientParams
    {
        public string ClientName { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public string PostLogoutRedirectUri { get; set; }
    }

    public class MoviesApiScopeParams
    {
        public string ScopeName { get; set; }

        public string ScopeDisplayName { get; set; }
    }
}