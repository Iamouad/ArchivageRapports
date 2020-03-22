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

        public ActionResult Index(string mail)
        {
            Entities entities = new Entities();
            AspNetUser user = entities.AspNetUsers.Where(usr => usr.Email.Equals(mail)).FirstOrDefault();
            if(user.AspNetRoles.Count > 0)
            {
                switch (user.AspNetRoles.ElementAt(0).Name)
                {
                    case "ADMIN":
                        return RedirectToAction("Index", "Admin");
                    case "PROFESSOR":
                        return RedirectToAction("Index", "Professor");
                    default:
                        return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Login", "Account");
        }
    }
}