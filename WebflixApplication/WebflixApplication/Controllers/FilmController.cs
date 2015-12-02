using Newtonsoft.Json;
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
            WebflixContext wfcontext = new WebflixContext();
            var film = wfcontext.FILMs.Find(id);
            var idRealisateur = film.IDREALISATEUR;
            var realisateur = wfcontext.PERSONNESFILMs.Find(idRealisateur);
            ShowFilmViewModel sfvm = new ShowFilmViewModel(film, realisateur);

            return View(sfvm);
        }

        public ActionResult ShowRentMovie(int id, char type, String message)
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

        //ajax needs to be change to start searching from 3 characters min.
        public ActionResult SearchMovie(String query)
        {
            using (var webflixContext = new WebflixContext())
            {
                return Json(webflixContext.searchMovie(query));
            }

        }

        public ActionResult AdvanceSearchMovie(String title, String actor, String realisator, String genre, String country, String language, String year)
        {
            using (var webflixContext = new WebflixContext())
            {
                return Json(webflixContext.AdvanceSearchMovie(title, actor, realisator, genre, country, language, year));
            }
        }

        public ActionResult AnalyzeData(String AgeGroup, String Province, String DayOfWeek, String Month)
        {
            int groupeAge = (AgeGroup.Equals("ALL") || AgeGroup.Equals("")) ? -1 : Int32.Parse(AgeGroup);
            int dayOfWeek = (DayOfWeek.Equals("ALL") || DayOfWeek.Equals("")) ? -1 : Int32.Parse(DayOfWeek);
            int month = (Month.Equals("ALL") || Month.Equals("")) ? -1 : Int32.Parse(Month);
            String prov = (String.IsNullOrEmpty(Province)) ? "ALL" : Province;

            using (var webflixWarehouseContext = new WebflixWarehouseContext())
            {
                return Json(webflixWarehouseContext.analyzeData(groupeAge, prov, dayOfWeek, month));
            }
        }

        public ActionResult RentMovie(int idFilm, int idClient)
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
                        copie.DISPONIBLE = false; // Apres la location
                        webflixContext.SaveChanges();
                        return RedirectToAction("ShowRentMovie", "Film", new { id = location.IDLOCATION, type = 'S', message = "Votre location a été effectué avec success le" + location.DATEDELOCATION });
                    }
                    catch (Exception e)
                    {
                        message = e.Message;
                        return RedirectToAction("ShowRentMovie", "Film", new { id = film.IDFILM, type = 'E', message = message });
                    }
                }
                else if (nombreLocationCourante >= clientMaxLoaction)
                {
                    return RedirectToAction("ShowRentMovie", "Film", new { id = film.IDFILM, type = 'E', message = "Vous avez atteint le nombre de copie permis par votre forfait." });
                }
                else
                {
                    return RedirectToAction("ShowRentMovie", "Film", new { id = film.IDFILM, type = 'E', message = "Il n'y a plus de copie disponible pour le film que vous souhaitez louer." });
                }
            }
        }
    }
}
