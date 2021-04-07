using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Client.Models;

namespace MoviesApp.Client.Clients
{
    public interface IMoviesClient
    {
        Task<List<MovieViewModel>> GetMoviesAsync();

        Task<MovieViewModel> GetMovieAsync(int id);

        Task<MovieViewModel> CreateMovieAsync(MovieViewModel movie);

        Task<MovieViewModel> UpdateMovieAsync(MovieViewModel movie);

        Task DeleteMovieAsync(int id);
    }
}
