using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebflixApplication.Models;

namespace WebflixApplication.ViewModels
{
    public class ShowFilmViewModel
    {
        public FILM film { get; set; }
        public PERSONNESFILM realisateur { get; set; }

        public ShowFilmViewModel(FILM film, PERSONNESFILM realisateur)
        {
            this.film = film;
            this.realisateur = realisateur;
        }
    }
}