namespace MoviesApp.Client.Options
{
    public class TokenClientOptions
    {
        public const string SectionKey = "TokenClient";
        
        public string Address { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Scope { get; set; }
    }
}