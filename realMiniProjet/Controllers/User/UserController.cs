using Microsoft.AspNet.Identity;
using realMiniProjet.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realMiniProjet.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        
        // GET: User

        public ActionResult Index()
        {

            if (User.IsInRole("ADMIN"))
                return RedirectToAction("Index", "Admin");
            else if (User.IsInRole("PROFESSOR"))
                return RedirectToAction("Index", "Professor");
            else if (User.IsInRole("STUDENT"))
                return RedirectToAction("Index", "Student");
            else
                return RedirectToAction("Login", "Account");
        }
    }
}