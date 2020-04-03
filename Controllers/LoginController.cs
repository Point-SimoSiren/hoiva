using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Hoivasovellus.Models;

namespace Hoivasovellus.Controllers
{
    public class LoginController : Controller
    {


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Auth(Hoivasovellus.Models.User userModel)
        {
            using (hoivadbEntities2 db = new hoivadbEntities2())
            {

                var userDetails = db.User.Where
               (x => x.UserName == userModel.UserName && x.Password == userModel.Password)
               .FirstOrDefault();

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Väärä käyttäjätunnus tai salasana.";

                    return View("Index", userModel);
                }
                else
                {
                    Session["UserID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Tapahtumat");
                }
            }


        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}