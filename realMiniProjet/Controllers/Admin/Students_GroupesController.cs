using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using realMiniProjet.Models.Entities;

namespace realMiniProjet.Controllers.Admin
{
    public class Students_GroupesController : Controller
    {
        private Entities db = new Entities();

        // GET: Students_Groupes
        public ActionResult Index()
        {
            var students_Groupes = db.Students_Groupes.Include(s => s.Groupe).Include(s => s.Student);
            return View(students_Groupes.ToList());
        }

        // GET: Students_Groupes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Groupes students_Groupes = db.Students_Groupes.Find(id);
            if (students_Groupes == null)
            {
                return HttpNotFound();
            }
            return View(students_Groupes);
        }

        // GET: Students_Groupes/Create
        public ActionResult Create()
        {
            ViewBag.Id_groupe = new SelectList(db.Groupes, "Id", "Id_prof");
            ViewBag.Id_student = new SelectList(db.Students, "Id", "UserId");
            return View();
        }

        // POST: Students_Groupes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_groupe,Id_student")] Students_Groupes students_Groupes)
        {
            if (ModelState.IsValid)
            {
                db.Students_Groupes.Add(students_Groupes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_groupe = new SelectList(db.Groupes, "Id", "Id_prof", students_Groupes.Id_groupe);
            ViewBag.Id_student = new SelectList(db.Students, "Id", "UserId", students_Groupes.Id_student);
            return View(students_Groupes);
        }

        // GET: Students_Groupes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Groupes students_Groupes = db.Students_Groupes.Find(id);
            if (students_Groupes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_groupe = new SelectList(db.Groupes, "Id", "Id_prof", students_Groupes.Id_groupe);
            ViewBag.Id_student = new SelectList(db.Students, "Id", "UserId", students_Groupes.Id_student);
            return View(students_Groupes);
        }

        // POST: Students_Groupes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_groupe,Id_student")] Students_Groupes students_Groupes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students_Groupes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_groupe = new SelectList(db.Groupes, "Id", "Id_prof", students_Groupes.Id_groupe);
            ViewBag.Id_student = new SelectList(db.Students, "Id", "UserId", students_Groupes.Id_student);
            return View(students_Groupes);
        }

        // GET: Students_Groupes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Groupes students_Groupes = db.Students_Groupes.Find(id);
            if (students_Groupes == null)
            {
                return HttpNotFound();
            }
            return View(students_Groupes);
        }

        // POST: Students_Groupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students_Groupes students_Groupes = db.Students_Groupes.Find(id);
            db.Students_Groupes.Remove(students_Groupes);
            db.SaveChanges();
            return RedirectToAction("Index");
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
}
