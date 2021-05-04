using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using TokenClientOptions = MoviesApp.Client.Options.TokenClientOptions;

namespace MoviesApp.Client.Clients
{
    public class TokenClient : ITokenClient
    {
        private readonly HttpClient _httpClient;
        private readonly TokenClientOptions _tokenClientOptions;

        public TokenClient(HttpClient httpClient, IOptions<TokenClientOptions> tokenClientOptions)
        {
            _httpClient = httpClient;
            _tokenClientOptions = tokenClientOptions.Value;
        }
        
        public async Task<TokenResponse> GetTokenAsync()
        {
            var tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = _tokenClientOptions.Address,
                ClientId = _tokenClientOptions.ClientId,
                ClientSecret = _tokenClientOptions.ClientSecret,
                Scope = _tokenClientOptions.Scope
            };

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

            return tokenResponse;
        }
    }
}