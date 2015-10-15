using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebflixApplication.Models;

namespace WebflixApplication.ViewModels
{
    public class RentFilmViewModel
    {
        public LOCATION location { get; set; }
        public FILM film { get; set; }
        public char type { get; set; }
        public String message { get; set; }

        public RentFilmViewModel(LOCATION location, char type, String message)
        {
            this.location = location;
            this.type = type;
            this.message = message;
        }

        public RentFilmViewModel(FILM film, char type, String message)
        {
            this.film = film;
            this.type = type;
            this.message = message;
        }
    }
}