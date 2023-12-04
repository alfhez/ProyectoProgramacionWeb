using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyMoviesList.Shared.Models
{
    public class Movie
    {
        public string? Titulo { get; set; }
        public string? Sinopsis { get; set; }
        public string? ImageURL { get; set; }
        public float Rating { get; set; }
        public List<String>? Generos { get; set; }
    }
}
