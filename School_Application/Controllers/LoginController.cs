using School_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Application.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(School_Application.Models.User userModel)
        {
            using (SchoolEntities dalObj = new SchoolEntities())
            {
                var userDetails = dalObj.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                  //  userModel.LoginErrorMessage = "Wrong User Name or Password.";
                    return View("index", userModel);
                }

                else
                {
                    Session["userId"] = userDetails.Userid;
                    Session["userName"] = userDetails.UserName;

                    return RedirectToAction("Index", "Admin");
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