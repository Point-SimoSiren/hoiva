using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hoivasovellus.Controllers
{
    public class InfotController : Controller
    {


        public ActionResult About()
        {
            ViewBag.Message = "Lapsiperheille, perhepäivähoitajille ja omaishoitajille tarkoitettu asiakastietojen hallintasovellus";
            return View();
        }
    }
}