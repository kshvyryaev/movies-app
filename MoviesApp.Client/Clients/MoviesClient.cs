using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoviesApp.Client.Models;
using MoviesApp.Client.Options;
using Newtonsoft.Json;

namespace MoviesApp.Client.Clients
{
    public class MoviesClient : IMoviesClient
    {
        private readonly HttpClient _httpClient;
        private readonly MoviesClientOptions _moviesClientOptions;

        public MoviesClient(HttpClient httpClient, IOptions<MoviesClientOptions> moviesClientOptions)
        {
            _httpClient = httpClient;
            _moviesClientOptions = moviesClientOptions.Value;
        }
        
        public async Task<List<MovieViewModel>> GetMoviesAsync()
        {
            var response = await _httpClient.GetAsync(_moviesClientOptions.Uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<MovieViewModel>>(content);

            return result;
        }

        public async Task<MovieViewModel> GetMovieAsync(int id)
        {
            var requestUri = $"{_moviesClientOptions.Uri}/{id}";
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
            
            var response = await _httpClient.PostAsync(_moviesClientOptions.Uri, requestContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MovieViewModel>(content);

            return result;
        }

        public async Task<MovieViewModel> UpdateMovieAsync(MovieViewModel movie)
        {
            var requestUri = $"{_moviesClientOptions.Uri}/{movie.Id}";
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
            var requestUri = $"{_moviesClientOptions.Uri}/{id}";
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
