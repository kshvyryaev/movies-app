using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Client.Clients;
using MoviesApp.Client.Models;

namespace MoviesApp.Client.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesClient _moviesClient;

        public MoviesController(IMoviesClient moviesClient)
        {
            _moviesClient = moviesClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _moviesClient.GetMoviesAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieViewModel = await _moviesClient.GetMovieAsync(id.Value);

            if (movieViewModel == null)
            {
                return NotFound();
            }

            return View(movieViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                movieViewModel = await _moviesClient.CreateMovieAsync(movieViewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(movieViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieViewModel = await _moviesClient.GetMovieAsync(id.Value);

            if (movieViewModel == null)
            {
                return NotFound();
            }

            return View(movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] MovieViewModel movieViewModel)
        {
            if (id != movieViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                movieViewModel = await _moviesClient.UpdateMovieAsync(movieViewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(movieViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieViewModel = await _moviesClient.GetMovieAsync(id.Value);

            if (movieViewModel == null)
            {
                return NotFound();
            }

            return View(movieViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieViewModel = await _moviesClient.GetMovieAsync(id);
            await _moviesClient.DeleteMovieAsync(movieViewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MovieViewModelExists(int id)
        {
            var movieViewModel = await _moviesClient.GetMovieAsync(id);

            return movieViewModel != null;
        }
    }
}
