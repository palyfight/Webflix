using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebflixApplication.Models
{
    public class FilmRecommendation
    {
        public int id_film_J { get; set; }
        public double correlation { get; set; }
    }
}