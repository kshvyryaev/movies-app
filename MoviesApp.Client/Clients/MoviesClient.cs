using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Client.Models;

namespace MoviesApp.Client.Clients
{
    public class MoviesClient : IMoviesClient
    {
        public Task<List<MovieViewModel>> GetMoviesAsync()
        {
            var moviesMock = new List<MovieViewModel>
            {
                new MovieViewModel
                {
                    Id = 1,
                    Title = "Title 1",
                    Genre = "Genre 1",
                    Rating = "1.1",
                    ReleaseDate = new DateTime(2001, 1, 1),
                    ImageUrl = "Image url 1",
                    Owner = "Owner 1"
                }
            };

            return Task.FromResult(moviesMock);
        }

        public Task<MovieViewModel> GetMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieViewModel> CreateMovieAsync(MovieViewModel movie)
        {
            throw new NotImplementedException();
        }

        public Task<MovieViewModel> UpdateMovieAsync(MovieViewModel movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
