using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MoviesApp.Client.Models;
using Newtonsoft.Json;

namespace MoviesApp.Client.Clients
{
    public class MoviesClient : IMoviesClient
    {
        private const string ResourceUri = "/api/movies";
        
        private readonly HttpClient _httpClient;

        public MoviesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<MovieViewModel>> GetMoviesAsync()
        {
            var response = await _httpClient.GetAsync(ResourceUri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<MovieViewModel>>(content);

            return result;
        }

        public async Task<MovieViewModel> GetMovieAsync(int id)
        {
            var requestUri = $"{ResourceUri}/{id}";
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MovieViewModel>(content);

            return result;
        }

        public async Task<MovieViewModel> CreateMovieAsync(MovieViewModel movie)
        {
            var requestJson = JsonConvert.SerializeObject(movie);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            
            var response = await _httpClient.PostAsync(ResourceUri, requestContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MovieViewModel>(content);

            return result;
        }

        public async Task<MovieViewModel> UpdateMovieAsync(MovieViewModel movie)
        {
            var requestUri = $"{ResourceUri}/{movie.Id}";
            var requestJson = JsonConvert.SerializeObject(movie);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            
            var response = await _httpClient.PutAsync(requestUri, requestContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MovieViewModel>(content);

            return result;
        }

        public async Task DeleteMovieAsync(int id)
        {
            var requestUri = $"{ResourceUri}/{id}";
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
