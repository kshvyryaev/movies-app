using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.Api.Models;

namespace MoviesApp.Api.Data
{
    public static class MoviesApiContextSeed
    {
        public static void Seed(MoviesApiContext context)
        {
            if (context.Movies.Any())
            {
                return;
            }
            
            var movies = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Title 1",
                    Genre = "Genre 1",
                    Rating = "1.1",
                    ReleaseDate = new DateTime(2001, 1, 1),
                    ImageUrl = "Image url 1",
                    Owner = "Owner 1"
                },
                new Movie
                {
                    Id = 2,
                    Title = "Title 2",
                    Genre = "Genre 2",
                    Rating = "2.2",
                    ReleaseDate = new DateTime(2002, 2, 2),
                    ImageUrl = "Image url 2",
                    Owner = "Owner 2"
                },
                new Movie
                {
                    Id = 3,
                    Title = "Title 3",
                    Genre = "Genre 3",
                    Rating = "3.3",
                    ReleaseDate = new DateTime(2003, 3, 3),
                    ImageUrl = "Image url 3",
                    Owner = "Owner 3"
                }
            };

            context.Movies.AddRange(movies);
            context.SaveChanges();
        }
    }
}