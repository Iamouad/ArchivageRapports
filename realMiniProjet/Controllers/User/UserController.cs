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
            String id = User.Identity.GetUserId();
            Entities entities = new Entities();
            AspNetUser user = entities.AspNetUsers.Find(id);
            if(user.AspNetRoles != null)
            {
                switch (user.AspNetRoles.FirstOrDefault().Name)
                {
                    case "ADMIN":
                        return RedirectToAction("Index", "Admin");
                    case "PROFESSOR":
                        return RedirectToAction("Index", "Professor");
                    case "STUDENT":
                        return RedirectToAction("Index", "Student");
                    default:
                        return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Login", "Account");
        }
    }
}