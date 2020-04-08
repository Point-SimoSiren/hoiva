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
            using (hoivaEntities db = new hoivaEntities())
            {

                var user = db.User.Where
               (x => x.UserName == userModel.UserName && x.Password == userModel.Password)
               .FirstOrDefault();

                if (user == null)
                {
                    userModel.LoginErrorMessage = "Väärä käyttäjätunnus tai salasana.";

                    return View("Index", userModel);
                }
                else
                {
                    // Onnistunut kirjautuminen normaali user

                    if (user.AdminUser == false)
                    {
                        Session["basicUser"] = user.UserID;
                        return RedirectToAction("Index", "Tapahtumat");
                    }

                    // Onnistunut kirjautuminen admin user

                    else
                    {
                        Session["adminUser"] = user.UserID;
                        Session["basicUser"] = user.UserID;
                        return RedirectToAction("Index", "Users");
                    }
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