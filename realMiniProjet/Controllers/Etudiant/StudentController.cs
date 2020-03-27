using Microsoft.AspNet.Identity;
using realMiniProjet.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realMiniProjet.Controllers.Etudiant
{
    [Authorize(Roles =("STUDENT"))]
    public class StudentController : Controller
    {
        Entities db = new Entities();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult uploadReport(String idGrp)
        {
            int groupeId = Convert.ToInt32(idGrp);
            String UserId = User.Identity.GetUserId();
            Student std = db.Students.Where(s => s.UserId.Equals(UserId)).FirstOrDefault();
            Students_Groupes sg = db.Students_Groupes.Where(g => g.Id_groupe.Equals(groupeId) && g.Id_student.Equals(std.Id)).FirstOrDefault();
            if (sg == null)
            {
                return RedirectToAction("Index", "Student");
            }
            ViewBag.type = new SelectList(db.Type_Reports, "Id_type", "Type");
            ViewBag.idGrp = groupeId;

            return View("UploadReport");
        }

        [HttpPost]
        public ActionResult uploadFile(HttpPostedFileBase file, string idGrp, Int32? Id_Type, string sujet)
        {
            int groupeId = Int32.Parse(idGrp);

            String UserId = User.Identity.GetUserId();
            Student std = db.Students.Where(s => s.UserId.Equals(UserId)).FirstOrDefault();
            Students_Groupes sg = db.Students_Groupes.Where(g => g.Id_groupe == groupeId && g.Id_student.Equals(std.Id)).FirstOrDefault();

            if (sg == null)
            {
                return RedirectToAction("Index", "Student");
            }

            Boolean extISGood = true;
            //extenstion.ToLower() == "pdf" && extenstion.ToLower() == "docx";

            Groupe grp = db.Groupes.Find(groupeId);
            if (file != null && file.ContentLength > 0 && extISGood)
            {
                try
                {
                    string name = Path.GetFileName(file.FileName);
                    string extenstion = Path.GetExtension(name);
                    Random random = new Random();
                    int Hashedname = (name + random.Next()).GetHashCode();
                    string newName = Hashedname + extenstion;
                    string path = Path.Combine(Server.MapPath("~/Content/Files"), newName);

                    file.SaveAs(path);

                    ViewBag.Id_filiere = grp.Id_fil;
                    ViewBag.Id_niv = grp.Id_niv;
                    ViewBag.Id_prof = grp.Id_prof;
                    ViewBag.Id_type = Id_Type.Value;
                    ViewBag.Sujet = sujet;
                    ViewBag.ReportPath = newName;

                    Report repport = db.Reports.Where(r => r.Id_grp == groupeId).FirstOrDefault();
                    if (repport != null)
                    {
                        var fullPath = Server.MapPath("~/Content/Files/" + repport.ReportPath);
                        ViewBag.exist = fullPath;
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        db.Reports.Remove(repport);
                    }


                    Report newRepport = new Report();

                    newRepport.Id_grp = grp.Id;
                    newRepport.Id_filiere = grp.Id_fil;
                    newRepport.Id_niv = grp.Id_niv;
                    newRepport.Id_prof = grp.Id_prof;
                    newRepport.Id_type = Id_Type.Value;
                    newRepport.Sujet = sujet;
                    newRepport.ReportPath = newName;
                    newRepport.DateDepot = DateTime.Now;

                    if (newRepport.DateDepot.Month < 7)
                    {
                        newRepport.DateUniv = (newRepport.DateDepot.Year - 1) + "-" + newRepport.DateDepot.Year;
                    }
                    else
                    {
                        newRepport.DateUniv = newRepport.DateDepot.Year + "-" + (newRepport.DateDepot.Year + 1);
                    }

                    db.Reports.Add(newRepport);

                    db.SaveChanges();
                    ViewBag.Message = "File uploaded successfully";
                    ViewBag.path = Path.Combine("/Content/Files", Path.GetFileName(file.FileName));
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.ToString();
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            ViewBag.type = new SelectList(db.Type_Reports, "Id_type", "Type");
            return View("UploadReport");
        }

    }
}