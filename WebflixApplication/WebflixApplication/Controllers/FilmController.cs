using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
