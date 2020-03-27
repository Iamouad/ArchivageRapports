using ClosedXML.Excel;
using Microsoft.AspNet.Identity.Owin;
using realMiniProjet.Models;
using realMiniProjet.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspNetUser = realMiniProjet.Models.Entities.AspNetUser;

namespace realMiniProjet.Controllers.UploadStudent
{
    public class UploadStudentsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public UploadStudentsController()
        {
        }
        public UploadStudentsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: UoloadStudents
        public ActionResult Index()
        {
            Entities dc = new Entities();

            ViewBag.filieres = dc.Filieres;
            ViewBag.niveaux = dc.Levels;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> uploadFile(HttpPostedFileBase file, String niv, String fil)
        {
            List<AspNetUser> users = new List<AspNetUser>();
            Entities dc = new Entities();

            ViewBag.filieres = dc.Filieres;
            ViewBag.niveaux = dc.Levels;

            int addedStudent = 0;

            if (file != null && file.ContentLength > 0)
                try
                {
                    // string path = Path.Combine(Server.MapPath("~/Content/Files"),
                    //        Path.GetFileName(file.FileName));
                    //  file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                    ViewBag.path = Path.Combine("/Content/Filesd", Path.GetFileName(file.FileName));
                    ViewBag.fileMime = file.ContentType;
                    ViewBag.fileSize = UserManager.ToString();

                    Stream fileStream = file.InputStream;
                    var stream = file.InputStream;
                    if (stream.Length != 0)
                    {
                        //handle the stream here
                        using (XLWorkbook excelWorkbook = new XLWorkbook(stream))
                        {
                            var name = excelWorkbook.Worksheet(1).Name;
                            //do more things whatever you like as you now have a handle to the entire workbook.
                            var row = excelWorkbook.Worksheet(1).Row(1);

                            while (!row.Cell(1).IsEmpty())
                            {
                                AspNetUser user1 = new AspNetUser();
                                ApplicationUser user = new ApplicationUser();
                                Student student = new Student();

                                user1.FirstName = row.Cell(1).GetString();
                                user1.LastName = row.Cell(2).GetString();
                                user.PhoneNumber = row.Cell(4).GetString();
                                user.Email = row.Cell(3).GetString();
                                user.UserName = user.Email;
                                user.SecurityStamp = "bader";
                                student.Cne = row.Cell(5).GetString();

                                var result = await UserManager.CreateAsync(user, "Password123@");

                                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                                user1.PasswordHash = user.PasswordHash;
                                user1.Id = user.Id;
                                user1.LockoutEnabled = user.LockoutEnabled;
                                user1.PhoneNumber = user.PhoneNumber;
                                user1.TwoFactorEnabled = user.TwoFactorEnabled;
                                user1.EmailConfirmed = user.EmailConfirmed;
                                user1.AccessFailedCount = user.AccessFailedCount;
                                user1.UserName = user.UserName;
                                user1.Email = user.Email;
                                user1.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                                user1.SecurityStamp = user.SecurityStamp = "bader";

                                ViewBag.fileSize = result.ToString();

                                var exist = from u in dc.AspNetUsers where u.UserName == user1.UserName select u;

                                if (exist.FirstOrDefault() == null)
                                {

                                    student.Id_niv = Int32.Parse(niv);
                                    student.Id_fil = Int32.Parse(fil);

                                    dc.AspNetUsers.Add(user1);
                                    student.UserId = user1.Id;
                                    dc.Students.Add(student);
                                    ++addedStudent;
                                }

                                row = row.RowBelow();
                            }

                            dc.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            ViewBag.niveau = "Added students : " + addedStudent;
            return View("Index", users);
        }
    }
}