namespace MoviesApp.Api.Options
{
    public class AuthorizationOptions
    {
        public const string SectionKey = "Authorization";
        public const string ClientIdPolicyName = "ClientIdPolicy";

        public ClientIdPolicyParams ClientIdPolicy { get; set; }
    }

    public class ClientIdPolicyParams
    {
        public string ClientIdType { get; set; }

        public string ClientIdValue { get; set; }
    }
}