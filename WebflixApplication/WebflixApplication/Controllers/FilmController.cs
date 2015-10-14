using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            query = query.Replace(",", "|");
            Match isNumber = Regex.Match(query, @"^[0-9-|]*$");
            string json = null;

            using (var webflixContext = new WebflixContext())
            {
                Dictionary<Decimal, FILM> result;
                //Always search by titles
                var titlesFilm  = webflixContext.getFilmByTitle(query).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First()) ;

                if (!isNumber.Success)
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

                    result = titlesFilm.Concat(actorFilm).Concat(realisatorFilm).Concat(genreFilm).Concat(contryFilm).Concat(languageFilm).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);

                }
                else
                {
                    //if plusieur date need to loop
                    //Search by years
                    result = titlesFilm;
                    foreach (String dates in query.Split(new String[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        
                        string[] stringSeparators = new string[] { "-" };
                        String[] years = dates.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                        if (years[0].Length == 4)
                        {
                            String min = years[0];
                            String max = years[0];
                            if (years.Length > 1 && years[1].Length == 4)
                            {
                                max = years[1];
                            }

                            DateTime starts = DateTime.ParseExact(min + "-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            DateTime ends = DateTime.ParseExact(max + "-12-31", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);

                            var dateFilm = webflixContext.getFilmByDate(starts, ends).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                            result = result.Concat(dateFilm).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);
                        }
    
                    }

                }

                json = JsonConvert.SerializeObject(result);
                return Json(new { response = json });
            }
          
        }

        public ActionResult AdvanceSearchMovie(String title, String actor, String realisator, String genre, String country, String language, String dates)
        {
            Dictionary<Decimal, FILM> result =  new Dictionary<decimal,FILM>();
            string json = "";

            using (var webflixContext = new WebflixContext())
            {
                if (title.Length > 0)
                {
                    var titlesFilm = webflixContext.getFilmByTitle(title).GroupBy(item => item.IDFILM).ToDictionary(item => item.Key, item => item.First());
                    
                } 
            }

       
            return null;
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
