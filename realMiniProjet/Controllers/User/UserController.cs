using Microsoft.AspNet.Identity;
using realMiniProjet.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace realMiniProjet.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        Entities db = new Entities();   
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

        public ActionResult MyGroups()
        {
            String id = User.Identity.GetUserId();
            ViewBag.path = Server.MapPath("~/Content/Files/");
            if (User.IsInRole("STUDENT"))
            {
                List<Groupe> groupeStd = new List<Groupe>();

                foreach (Students_Groupes std in db.Students_Groupes)
                {
                    if (std.Student.UserId.Equals(id))
                    {
                        Groupe grp = db.Groupes.Find(std.Id_groupe);
                        groupeStd.Add(grp);

                    }
                }
                return View("groups", groupeStd);
            }
            var groupes = db.Groupes.Include(grp => grp.Filiere).Include(grp => grp.Level).Include(grp => grp.Reports);
            return View("groups", groupes.Where(grp => grp.Id_prof.Equals(id)).ToList());
        }


        
    }
}