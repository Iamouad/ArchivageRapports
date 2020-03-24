using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using realMiniProjet.Models.Entities;

namespace realMiniProjet.Controllers
{
    public class GroupesController : Controller
    {
        private Entities db = new Entities();

        // GET: Groupes
        public ActionResult Index()
        {
            var groupes = db.Groupes.Include(g => g.AspNetUser);
            return View(groupes.ToList());
        }

        // GET: Groupes/Details/5
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

        // GET: Groupes/Create
        public ActionResult Create()
        {
            ViewBag.Id_prof = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Groupes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_prof,Id_fil,Id_level")] Groupe groupe)
        {
            if (ModelState.IsValid)
            {
                db.Groupes.Add(groupe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_prof = new SelectList(db.AspNetUsers, "Id", "Email", groupe.Id_prof);
            return View(groupe);
        }

        // GET: Groupes/Edit/5
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
            ViewBag.Id_prof = new SelectList(db.AspNetUsers, "Id", "Email", groupe.Id_prof);
            return View(groupe);
        }

        // POST: Groupes/Edit/5
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
            ViewBag.Id_prof = new SelectList(db.AspNetUsers, "Id", "Email", groupe.Id_prof);
            return View(groupe);
        }

        // GET: Groupes/Delete/5
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

        // POST: Groupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groupe groupe = db.Groupes.Find(id);
            db.Groupes.Remove(groupe);
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
