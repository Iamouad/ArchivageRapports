using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using realMiniProjet.Models.Entities;
using System.Data;
using System.Data.Entity;


namespace realMiniProjet.Controllers.Professor
{
    public class ProfessorController : Controller
    {
        Entities db = new Entities();
        // GET: Professor
        public ActionResult Index()
        {
            return View("profil");
        }

        public ActionResult MyGroups()
        {
            
            String id = User.Identity.GetUserId();
            var groupes = db.Groupes.Include(grp => grp.Filiere).Include(grp => grp.Level).Include(grp => grp.Reports);
            return View("groups", groupes.Where(grp => grp.Id_prof.Equals(id)).ToList());
        }


    }
}