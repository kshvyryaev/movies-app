using System.Threading.Tasks;
using IdentityModel.Client;

namespace MoviesApp.Client.Clients
{
    public interface ITokenClient
    {
        Task<TokenResponse> GetTokenAsync();
    }
}