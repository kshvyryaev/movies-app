using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Api.Data;
using MoviesApp.Api.Models;
using AuthorizationOptions = MoviesApp.Api.Options.AuthorizationOptions;

namespace MoviesApp.Api.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize(AuthorizationOptions.ApiClientIdPolicyName)]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesApiContext _context;

        public MoviesController(MoviesApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovie([FromRoute] int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
        
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie([FromBody] Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Movie>> PutMovie([FromRoute] int id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(x => x.Id == id);
        }
    }
}