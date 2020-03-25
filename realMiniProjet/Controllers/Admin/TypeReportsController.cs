using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using realMiniProjet.Models.Entities;

namespace realMiniProjet.Controllers.Admin
{
    [Authorize(Roles ="ADMIN")]
    public class TypeReportsController : Controller
    {
        private Entities db = new Entities();

        // GET: TypeReports
        public async Task<ActionResult> Index()
        {
            return View(await db.Type_Reports.ToListAsync());
        }

        // GET: TypeReports/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Reports type_Reports = await db.Type_Reports.FindAsync(id);
            if (type_Reports == null)
            {
                return HttpNotFound();
            }
            return View(type_Reports);
        }

        // GET: TypeReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_type,Type")] Type_Reports type_Reports)
        {
            if (ModelState.IsValid)
            {
                db.Type_Reports.Add(type_Reports);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(type_Reports);
        }

        // GET: TypeReports/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Reports type_Reports = await db.Type_Reports.FindAsync(id);
            if (type_Reports == null)
            {
                return HttpNotFound();
            }
            return View(type_Reports);
        }

        // POST: TypeReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_type,Type")] Type_Reports type_Reports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type_Reports).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(type_Reports);
        }

        // GET: TypeReports/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Reports type_Reports = await db.Type_Reports.FindAsync(id);
            if (type_Reports == null)
            {
                return HttpNotFound();
            }
            return View(type_Reports);
        }

        // POST: TypeReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Type_Reports type_Reports = await db.Type_Reports.FindAsync(id);
            db.Type_Reports.Remove(type_Reports);
            await db.SaveChangesAsync();
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
