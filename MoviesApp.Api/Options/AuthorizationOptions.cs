namespace MoviesApp.Api.Options
{
    public class AuthorizationOptions
    {
        public const string SectionKey = "Authorization";
        public const string ApiClientIdPolicyName = "ApiClientIdPolicy";

        public ApiClientIdPolicyParams ApiClientIdPolicy { get; set; }
    }

    public class ApiClientIdPolicyParams
    {
        public string ClientIdType { get; set; }

        public string[] ClientIdValues { get; set; }
    }
}