using realMiniProjet.Models;
using realMiniProjet.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realMiniProjet.Controllers
{
    public class SearchEngineController : Controller
    {
        // GET: SearchEngine
        public ActionResult Index()
        {
            Entities dbContext = new Entities();
            ViewBag.f = new SelectList(dbContext.Filieres, "Id_filiere", "Nom_filiere");
            ViewBag.niveau = new SelectList(dbContext.Levels , "Id_niveau", "Nom_niveau");
            ViewBag.type = new SelectList(dbContext.Type_Reports, "Id_type", "Type");

            ViewBag.prof = new SelectList(dbContext.AspNetUsers.Where(p => p.AspNetRoles.FirstOrDefault().Name.Equals("PROFESSOR")),
                "Id", "LastName");
            ViewBag.Au = new SelectList((from au in dbContext.ArchivedReports select au.DateUniv).Distinct());
            //dbContext.Filieres.Find(0).idFiliere
            ViewBag.FilId = "bae";
            ViewBag.SearchedBy = "d";
            ViewBag.SearchedText = "f";
            ViewBag.profId = "d";
            return View();
        }

        public JsonResult GetReports(Int32? FilId, Int32? Id_niveau, Int32?  Id_Type, String SearchedBy, String SearchedText, String profId)
        {

            Entities dbContext = new Entities();
            dbContext.Configuration.ProxyCreationEnabled = false;




            List<ArchivedReport> reports = dbContext.ArchivedReports.Where(r =>
            (!FilId.HasValue || r.Id_filiere == FilId)
            &&
            (!Id_niveau.HasValue || r.Id_niveau == Id_niveau)
            &&
            (!Id_Type.HasValue || r.Id_type == Id_Type)
            &&
           ((profId == "") || r.Id_prof == profId)
           &&
           r.Sujet.Contains(SearchedText)
           
           ).ToList();

            List<RepportDetails> repportsDetails = new List<RepportDetails>();

            RepportDetails repportDetails;
            foreach (ArchivedReport archivedReport in reports)
            {
                repportDetails = new RepportDetails();

                //archivedReport.Filiere.Nom_filiere;
                repportDetails.Filiere = dbContext.Filieres.Where(f => f.Id_filiere == archivedReport.Id_filiere).FirstOrDefault().Nom_filiere;
                repportDetails.niveau = dbContext.Levels.Where(n => n.Id_niveau == archivedReport.Id_niveau).FirstOrDefault().Nom_niveau;
                repportDetails.type = dbContext.Type_Reports.Where(t => t.Id_type == archivedReport.Id_type).FirstOrDefault().Type;
                repportDetails.Encadrant = dbContext.AspNetUsers.Where(p => p.Id == archivedReport.Id_prof).FirstOrDefault().LastName;
                repportDetails.Au = archivedReport.DateUniv;
                repportDetails.path = "/Content/Files/" + archivedReport.ReportPath;
                repportDetails.remarque = archivedReport.RemarqueProf;
                repportDetails.sujet = archivedReport.Sujet;
       

                repportsDetails.Add(repportDetails);

            }

            return Json(repportsDetails, JsonRequestBehavior.AllowGet);
        }
    }
}
