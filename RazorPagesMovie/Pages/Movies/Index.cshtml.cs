using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? MovieCategory { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            // Genre list
            IQueryable<string> genreQuery =
                from m in _context.Movie
                orderby m.Genre
                select m.Genre;

            // Category list
            IQueryable<string> categoryQuery =
                from m in _context.Movie
                orderby m.Category
                select m.Category;

            // Base movie query
            var movies = from m in _context.Movie
                         select m;

            // Filter by search string
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            // Filter by genre
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            // Filter by category
            if (!string.IsNullOrEmpty(MovieCategory))
            {
                movies = movies.Where(x => x.Category == MovieCategory);
            }

            // Build dropdown lists
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());

            // Final movie list
            Movie = await movies.ToListAsync();
        }
    }
}
