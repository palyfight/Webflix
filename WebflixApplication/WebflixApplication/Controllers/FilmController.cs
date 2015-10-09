using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult SearchMovie(String query)
        {
            return Json(new { response = "success" });
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
