using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebflixApplication.Models;

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
            return View(film);
        }

        //ajax needs to be change to start searching from 3 characters min.
        public ActionResult SearchMovie(String query)
        {

            Match isNumber = Regex.Match(query, @"^[0-9-]*$");

            using (var webflixContext = new WebflixContext())
            {
                //Always search by titles
                var titlesFilm  = webflixContext.getFilmByTitle(query).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First()) ;

                if (isNumber.Success)
                {
                    //if plusieur date need to loop
                    //Search by years
                    string[] stringSeparators = new string[] { "-" };
                    String[] years = query.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                    String min = years[0];
                    String max = years.Length > 1 ? years[1] : years[0];

                    //webflixContext.getFilmByDate(min, max).ToDictionary(item => item.IDFILM, item => item) ;
                }
                else 
                {
                    //search by actor
                    var actorFilm = webflixContext.getFilmByActor(query).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());

                    //search by realisator
                    var realisatorFilm = webflixContext.getFilmByRealisator(query).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());

                    //search by genre
                    var genreFilm = webflixContext.getFilmByGenre(query).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());

                    //search by country
                    var contryFilm = webflixContext.getFilmByCountry(query).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());

                    //search by language
                    var languageFilm = webflixContext.getFilmByLanguage(query).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());

                    var result = titlesFilm.Concat(actorFilm).Concat(realisatorFilm).Concat(genreFilm).Concat(contryFilm).Concat(languageFilm).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);
                }
                
                

                return Json(new { response = "success" });
            }
          
        }

        public ActionResult RentMovie(int idFilm, int idClient)
        {
            CLIENT client;
            COPIE copie;
            FILM film;
            using (var webflixContext = new WebflixContext())
            {
                client = webflixContext.CLIENTs.Find(idClient);
                film = webflixContext.FILMs.Find(idFilm);
                copie = film.COPIEs.Where(c => c.DISPONIBLE == true).First();
                int clientMaxLoaction = (int)client.FORFAIT.LOCATIONSMAX;
                int nombreLocationCourante = client.LOCATIONs.Count(l => l.DATERETOUR == null);
                if (copie != null && nombreLocationCourante < clientMaxLoaction)
                {
                    LOCATION location = new LOCATION();
                    location.CLIENT = client;
                    copie.DISPONIBLE = false;
                    location.COPIE = copie;
                    location.DATEDELOCATION = DateTime.Now;
                    webflixContext.LOCATIONs.Add(location);
                    webflixContext.SaveChanges();
                }
                return RedirectToAction("ShowFilm", "Film", new { id = copie.IDFILM });
            }
        }
    }
}
