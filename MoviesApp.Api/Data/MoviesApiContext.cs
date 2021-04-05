using Microsoft.EntityFrameworkCore;
using MoviesApp.Api.Models;

namespace MoviesApp.Api.Data
{
    public class MoviesApiContext : DbContext
    {
        public MoviesApiContext(DbContextOptions<MoviesApiContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}