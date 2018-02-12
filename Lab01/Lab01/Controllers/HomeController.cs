using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab01.Models;

namespace Lab01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       public ActionResult About()
        {
            ViewBag.Message = "Instrucciones de Uso";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Desarrolladores";

            return View();
        }


    }
}