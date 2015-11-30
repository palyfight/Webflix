using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebflixApplication.Controllers
{
    public class AnalysisController : Controller
    {
        //
        // GET: /Analysis/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdvanceSearchMovie(String title, String actor, String realisator, String genre, String country, String language, String year)
        {
            using (var webflixContext = new WebflixContext())
            {
                return Json(webflixContext.AdvanceSearchMovie(title, actor, realisator, genre, country, language, year));
            }
        }

    }
}
