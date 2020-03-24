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
            return View("test");
        }

        public ActionResult GetGroups()
        {
            
            String id = User.Identity.GetUserId();
            var groupes = db.Groupes.Include(grp => grp.Filiere).Include(grp => grp.Level).Include(grp => grp.Report);
            //var students = db.Students.Include(s => s.AspNetUser).Include(s => s.Filiere).Include(s => s.Level);

            ViewData["id"] = id;
            ViewBag.filieres = new SelectList(db.Filieres, "Id_filiere", "Nom_filiere");
            ViewBag.niveaux = new SelectList(db.Levels, "Id_niveau", "Nom_niveau");
            return View("groups", groupes.Where(grp => grp.Id_prof.Equals(id)).ToList());
        }
    }
}