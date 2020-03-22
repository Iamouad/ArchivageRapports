using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using realMiniProjet.Models.Entities;

namespace realMiniProjet.Controllers.Admin
{
    [Authorize(Roles ="ADMIN")]
    public class HandlingStudentsController : Controller
    {
        private Entities db = new Entities();

        // GET: HandlingStudents
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.AspNetUser).Include(s => s.Filiere).Include(s => s.Level);
            return View(students.ToList());
        }

        // GET: HandlingStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: HandlingStudents/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Id_fil = new SelectList(db.Filieres, "Id_filiere", "Nom_filiere");
            ViewBag.Id_niv = new SelectList(db.Levels, "Id_niveau", "Nom_niveau");
            return View();
        }

        // POST: HandlingStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cne,Id_fil,Id_niv")] Student student, string userName, string firstNAme, string lastName, string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                AspNetRole role = db.AspNetRoles.Where(rl => rl.Name.Equals("STUDENT")).FirstOrDefault();
                Random random = new Random();
                int id = random.Next(1, 1000);

                AspNetUser aspNetUser = new AspNetUser
                {
                    Email = Email,
                    FirstName = firstNAme,
                    LastName = lastName,
                    Id = Crypto.Hash("user" + id),
                    PasswordHash = Crypto.HashPassword(Password),
                    UserName = userName
                };
                db.AspNetUsers.Add(aspNetUser);
                aspNetUser.AspNetRoles.Add(role);
                student.UserId = aspNetUser.Id;
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", student.UserId);
            ViewBag.Id_fil = new SelectList(db.Filieres, "Id_filiere", "Nom_filiere", student.Id_fil);
            ViewBag.Id_niv = new SelectList(db.Levels, "Id_niveau", "Nom_niveau", student.Id_niv);
            return View(student);
        }

        // GET: HandlingStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", student.UserId);
            ViewBag.Id_fil = new SelectList(db.Filieres, "Id_filiere", "Nom_filiere", student.Id_fil);
            ViewBag.Id_niv = new SelectList(db.Levels, "Id_niveau", "Nom_niveau", student.Id_niv);
            return View(student);
        }

        // POST: HandlingStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Cne,Id_fil,Id_niv")] Student student, string userName, string firstName, string lastName, string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                AspNetUser modifiedUser = db.AspNetUsers.Find(student.UserId);
                if (!modifiedUser.LastName.Equals(lastName))
                {
                    modifiedUser.LastName = lastName;
                }
                if (!modifiedUser.FirstName.Equals(firstName))
                {
                    modifiedUser.FirstName = firstName;
                }
                if (!modifiedUser.Email.Equals(Email))
                {
                    modifiedUser.Email = Email;
                }
                if (!Crypto.VerifyHashedPassword(modifiedUser.PasswordHash ,Password))
                {
                    modifiedUser.PasswordHash = Crypto.HashPassword(Password);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Id", student.UserId);
            ViewBag.Id_fil = new SelectList(db.Filieres, "Id_filiere", "Nom_filiere", student.Id_fil);
            ViewBag.Id_niv = new SelectList(db.Levels, "Id_niveau", "Nom_niveau", student.Id_niv);
            return View(student);
        }

        // GET: HandlingStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            AspNetUser aspNetUser = db.AspNetUsers.Find(student.UserId);
            if (student == null || aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: HandlingStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            AspNetUser aspNetUser = db.AspNetUsers.Find(student.UserId);
            db.Students.Remove(student);          
            db.AspNetUsers.Remove(aspNetUser);
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
