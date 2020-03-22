using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using realMiniProjet.Models.Entities;

namespace realMiniProjet.Controllers.Admin
{
    public class HandlingGroupesController : Controller
    {
        private Entities db = new Entities();

        // GET: HandlingGroupes
        public ActionResult Index()
        {
            var groupes = db.Groupes.Include(g => g.AspNetUser);
            return View(groupes.ToList());
        }

        // GET: HandlingGroupes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupes.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        // GET: HandlingGroupes/Create
        public ActionResult Create()
        {
            ViewBag.Id_prof = new SelectList(db.AspNetUsers.Where(usr => usr.AspNetRoles.FirstOrDefault().Name.Equals("PROFESSOR")), "Id", "Email");
            ViewBag.Id_fil = new SelectList(db.Filieres, "Id_filiere", "Nom_filiere");
            ViewBag.Id_lev = new SelectList(db.Levels, "Id_niveau", "Nom_niveau");
            return View();
        }

        // POST: HandlingGroupes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_prof,Delais")] Groupe groupe, string id_fil, string id_lev, string[] stdid )
        {
            int id_f, id_l;
            List<Student> students;
           
            try
            {
                id_f = Int32.Parse(id_fil);
                id_l = Int32.Parse(id_lev);
            }
            catch (Exception ex)
            {
                id_f = 0;
                id_l = 0; 
            }
            if (ModelState.IsValid)
            {
                db.Groupes.Add(groupe);

                students = db.Students.Where(std => std.Filiere.Id_filiere == id_f && std.Level.Id_niveau == id_l).ToList();
                foreach (Student std in students)
                {
                    string stringid = "std" + std.Id;
                    if (Array.Exists(stdid, elt => elt.Equals(stringid)))
                    {
                        Console.WriteLine(std.Cne);
                        Students_Groupes elt = new Students_Groupes { Id_groupe = groupe.Id, Id_student = std.Id };
                        db.Students_Groupes.Add(elt);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.Id_prof = new SelectList(db.AspNetUsers.Where(usr => usr.AspNetRoles.FirstOrDefault().Name.Equals("PROFESSOR")), "Id", "Email", groupe.Id_prof);
            return View(groupe);
        }

        // GET: HandlingGroupes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupes.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_prof = new SelectList(db.AspNetUsers.Where(usr => usr.AspNetRoles.FirstOrDefault().Name.Equals("PROFESSOR")), "Id", "Email", groupe.Id_prof);
            return View(groupe);
        }

        // POST: HandlingGroupes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_prof,Delais")] Groupe groupe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_prof = new SelectList(db.AspNetUsers.Where(usr => usr.AspNetRoles.FirstOrDefault().Name.Equals("PROFESSOR")), "Id", "Email", groupe.Id_prof);
            return View(groupe);
        }

        // GET: HandlingGroupes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupes.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        // POST: HandlingGroupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groupe groupe = db.Groupes.Find(id);
            List<Students_Groupes> targetGrp = db.Students_Groupes.Where(grp => grp.Id_groupe == id).ToList();
            foreach(Students_Groupes students_Groupes in targetGrp)
            {
                Students_Groupes grp = db.Students_Groupes.Find(students_Groupes.Id);
                db.Students_Groupes.Remove(grp);
            }
            db.Groupes.Remove(groupe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        


        public JsonResult getStudents(string id_f, string id_l)
        {
            int id_fil, id_niv;
            
            try
            {
                id_fil = Int32.Parse(id_f);
                id_niv = Int32.Parse(id_l);
            }
            catch (Exception ex)
            {
                id_niv = 0;
                id_fil = 0;
            }
            
            var students = db.Students.Where(std=>std.Filiere.Id_filiere.Equals(id_fil) && std.Level.Id_niveau.Equals(id_niv))
            .Select(std => new stdModel
            {
                Id = std.Id,
                Cne = std.Cne,
                FirstName = std.AspNetUser.FirstName,
                LastName = std.AspNetUser.LastName,
                Email = std.AspNetUser.Email
            }).ToList();
           
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getMembers(string id)
        {
            int id_grp;

            try
            {
                id_grp = Int32.Parse(id);
            }
            catch (Exception ex)
            {
                id_grp = 0;
            }

            var etd_grps = db.Students_Groupes.Where(sg => sg.Id_groupe == id_grp);
            List<Student> students = new List<Student>();

            foreach(var sg in etd_grps)
            {
                students.Add(db.Students.Find(sg.Id_student));
            }

            var returnList = students.Select(std => new stdModel
            {
                FirstName = std.AspNetUser.FirstName,
                LastName = std.AspNetUser.LastName
            }).ToList();




            /*  return Json(db.AspNetUsers.Select(std => new stdModel
              {
                  FirstName = std.FirstName,
                  LastName = std.LastName
              }).ToList(), JsonRequestBehavior.AllowGet);*/
            return Json(returnList, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    internal class stdModel
    {
        public string Cne { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; internal set; }
    }
}
