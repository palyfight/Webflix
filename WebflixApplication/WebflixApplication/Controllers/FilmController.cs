﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebflixApplication.Models;
using WebflixApplication.ViewModels;

namespace WebflixApplication.Controllers
{
    public class FilmController : Controller
    {
        //
        // GET: /Film/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowFilm(int id)
        {
            if (Session["IDPERSONNE"] != null)
            {
                WebflixContext wfcontext = new WebflixContext();
                var film = wfcontext.FILMs.Find(id);
                var idRealisateur = film.IDREALISATEUR;
                var realisateur = wfcontext.PERSONNESFILMs.Find(idRealisateur);
                ShowFilmViewModel sfvm = new ShowFilmViewModel(film, realisateur);

                return View(sfvm);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult RentFilm(int id, char type, String message)
        {
            if (Session["IDPERSONNE"] != null)
            {
                WebflixContext wfcontext = new WebflixContext();
                RentFilmViewModel rwm;
                if (type == 'S')
                {
                    var location = wfcontext.LOCATIONs.Find(id);
                    var idRealisateur = location.COPIE.FILM.IDREALISATEUR;
                    var realisateur = wfcontext.PERSONNESFILMs.Find(idRealisateur);
                    rwm = new RentFilmViewModel(location, realisateur, type, message);
                }
                else
                {
                    var film = wfcontext.FILMs.Find(id);
                    var idRealisateur = film.IDREALISATEUR;
                    var realisateur = wfcontext.PERSONNESFILMs.Find(idRealisateur);
                    rwm = new RentFilmViewModel(film, realisateur, type, message);
                }

                return View(rwm);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //ajax needs to be change to start searching from 3 characters min.
        public ActionResult SearchMovie(String query)
        {
            using (var webflixContext = new WebflixContext())
            {
                return Json( webflixContext.searchMovie(query) );
            }
          
        }

        public ActionResult AdvanceSearchMovie(String title, String actor, String realisator, String genre, String country, String language, String year)
        {
            using (var webflixContext = new WebflixContext())
            {
                return Json(webflixContext.AdvanceSearchMovie(title,  actor,  realisator,  genre,  country,  language,  year));
            }           
        }

        public ActionResult RentMovie(int idFilm, int idClient)
        {
            if (Session["IDPERSONNE"] != null)
            {
                CLIENT client;
                COPIE copie;
                FILM film;
                String message;

                using (var webflixContext = new WebflixContext())
                {
                    client = webflixContext.CLIENTs.Find(idClient);
                    film = webflixContext.FILMs.Find(idFilm);
                    copie = film.COPIEs.Where(c => c.DISPONIBLE == true).FirstOrDefault();
                    int clientMaxLoaction = (int)client.FORFAIT.LOCATIONSMAX;
                    int nombreLocationCourante = client.LOCATIONs.Count(l => l.DATERETOUR == null);
                    LOCATION location = new LOCATION();
                    if (copie != null && nombreLocationCourante < clientMaxLoaction)
                    {
                        location.CLIENT = client;
                        location.COPIE = copie;
                        location.DATEDELOCATION = DateTime.Now;
                        webflixContext.LOCATIONs.Add(location);
                        try
                        {
                            webflixContext.SaveChanges();
                            return RedirectToAction("RentFilm", "Film", new { id = location.IDLOCATION, type = 'S', message = "Votre location a été effectué avec success le" + location.DATEDELOCATION });
                        }
                        catch (Exception e)
                        {
                            message = e.Message;
                            return RedirectToAction("RentFilm", "Film", new { id = film.IDFILM, type = 'E', message = message });
                        }

                        copie.DISPONIBLE = false;
                        webflixContext.SaveChanges();
                    }
                    return RedirectToAction("RentFilm", "Film", new { id = film.IDFILM, type = 'E', message = "Vous avez atteint le nombre de copie permis par votre forfait." });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
