using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMoviesList.Server.Data;
using MyMoviesList.Shared.Models;

namespace MyMoviesList.Server.Controllers
{
    //Agregamos seguridad de autenticación a nuestro controlador
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AllMoviesList : ControllerBase
    {
        private readonly AppicationDbContext context;
        public AllMoviesList(AppicationDbContext context)
        {
            this.context = context;
        }
        // GET: AllMoviesListController
        [HttpGet("AllMovies")]
        public async Task<List<Movie>> Movies()
        {
            List<Movie> movies = new List<Movie>();
            movies = await context.Movies.ToListAsync();
            return movies;
        }

        [HttpPost("Rating")]
        public async Task<IActionResult> RateMovie(Movie movie, float Rating)
        {

            movie.Rating = Rating;

            context.Entry(movie).State = EntityState.Modified;

            await context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
