using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyMoviesList.Clients.Pages
{
    public class MoviesModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("Movies/Index");
        }
    }
}