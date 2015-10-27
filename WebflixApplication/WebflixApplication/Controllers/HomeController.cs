using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebflixApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["IDPERSONNE"] != null)
            {
                ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

                using (var webflixContext = new WebflixContext())
                {
                    var films = webflixContext.FILMs.ToList();
                    return View(films);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowAdvanceSearchMovie()
        {
            return View();
        }
    }
}
