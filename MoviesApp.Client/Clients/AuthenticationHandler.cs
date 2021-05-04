using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace MoviesApp.Client.Clients
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly ITokenClient _tokenClient;

        public AuthenticationHandler(ITokenClient tokenClient)
        {
            _tokenClient = tokenClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenClient.GetTokenAsync();
            
            request.SetBearerToken(token.AccessToken);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}