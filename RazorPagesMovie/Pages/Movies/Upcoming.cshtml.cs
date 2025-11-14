using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class UpcomingModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public UpcomingModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> UpcomingMovies { get; set; }

        public async Task OnGetAsync()    // <-- corrected
        {
            UpcomingMovies = await _context.Movie
                .Where(m => m.IsUpcoming == true)
                .ToListAsync();
        }
    }
}
