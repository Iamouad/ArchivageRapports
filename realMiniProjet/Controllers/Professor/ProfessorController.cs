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
    [Authorize(Roles ="PROFESSOR")]
    public class ProfessorController : Controller
    {
        Entities db = new Entities();
        // GET: Professor
        public ActionResult Index()
        {
            return View("profil");
        }

        public ActionResult ArchiveReport(string idGrp)
        {
            string currentUsr = User.Identity.GetUserId();
            try
            {
                int id = Convert.ToInt32(idGrp);
                Report report = db.Reports.Where(rp => rp.Id_grp.Equals(id) && rp.Id_prof.Equals(currentUsr)).FirstOrDefault();
                if(report == null)
                {
                    throw new Exception("No report for now!!!");
                }
                else
                {
                    Session.Add("idReport", id);
                    ViewBag.sujet = report.Sujet;
                    ViewBag.idReport = id;
                    ViewBag.anneeUniv = report.DateUniv;
                    return View("archiveForm");
                }
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View("error");
            }
        }

        [HttpPost]
        public ActionResult ArchiveReport(string sujet, string remarques, string idReport, string dateUniv)
        {
            try
            {
                string currentUsr = User.Identity.GetUserId();
                int id = Convert.ToInt32(idReport);
                Report report = db.Reports.Where(rp => rp.Id_grp.Equals(id) && rp.Id_prof.Equals(currentUsr)).FirstOrDefault();
                if (report == null)
                {
                    throw new Exception("Operation can't be done!!!");
                }
                ArchivedReport archived = new ArchivedReport
                {
                    Id_prof = currentUsr,
                    DateUniv = dateUniv,
                    Id_filiere = report.Id_filiere,
                    Id_niv = report.Id_niv,
                    Id_type = report.Id_type,
                    Sujet = sujet,
                    RemarqueProf = remarques,
                    //////////////////:path of the report archived/////////////
                    ReportPath = report.ReportPath
                };
                Groupe grp = db.Groupes.Find(report.Id_grp);
                db.ArchivedReports.Add(archived);
                db.Reports.Remove(report);
                foreach(Students_Groupes students in db.Students_Groupes)
                {
                    if(students.Id_groupe == id)
                    {
                        db.Students_Groupes.Remove(students);
                    }
                }
                db.Groupes.Remove(grp);
                db.SaveChanges();
                return View("profil");

                }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View("error");
            }
        }






    }
}