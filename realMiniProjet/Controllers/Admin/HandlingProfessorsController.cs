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
using Microsoft.AspNet.Identity.Owin;
using realMiniProjet.Models;

namespace realMiniProjet.Controllers.Admin
{
    public class HandlingProfessorsController : Controller
    {
        private Entities db = new Entities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HandlingProfessorsController()
        {
        }

        public HandlingProfessorsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: HandlingProfessors
        public async Task<ActionResult> Index()
        {
            return View(await db.AspNetUsers.Where(usr => usr.AspNetRoles.FirstOrDefault().Name.Equals("PROFESSOR")).ToListAsync());
        }

        // GET: HandlingProfessors/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: HandlingProfessors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HandlingProfessors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,PasswordHash,PhoneNumber,UserName,FirstName,LastName")] AspNetUser aspNetUser)
        {
          
           
            if (ModelState.IsValid)
            {
                // AspNetUser user1 = new AspNetUser();
                ApplicationUser user = new ApplicationUser();
                
                user.PhoneNumber = aspNetUser.PhoneNumber;
                user.Email = aspNetUser.Email;
                user.UserName = aspNetUser.UserName;
                
                var result = await UserManager.CreateAsync(user, aspNetUser.PasswordHash);

                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                if (result.Succeeded)
                {
                    AspNetRole role = db.AspNetRoles.Where(rl => rl.Name.Equals("PROFESSOR")).FirstOrDefault();
                    AspNetUser userInfo = db.AspNetUsers.Find(user.Id);
                    userInfo.FirstName = aspNetUser.FirstName;
                    userInfo.LastName = aspNetUser.LastName;
                    userInfo.AspNetRoles.Add(role);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                
            }

            return View(aspNetUser);
        }

        // GET: HandlingProfessors/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: HandlingProfessors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }

        // GET: HandlingProfessors/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: HandlingProfessors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(id);
            db.AspNetUsers.Remove(aspNetUser);
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
