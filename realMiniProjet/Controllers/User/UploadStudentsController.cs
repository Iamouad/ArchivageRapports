using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
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
                                ApplicationUser user = new ApplicationUser();
                                string FirstName = row.Cell(1).GetString();
                                string LastName = row.Cell(2).GetString();
                                string PhoneNumber = row.Cell(4).GetString();
                                string Email = row.Cell(3).GetString();
                                string cne = row.Cell(5).GetString();
                                user.Email = Email;
                                user.UserName = Email;
                                user.PhoneNumber = PhoneNumber;
                                var exist = from u in dc.AspNetUsers where u.UserName == user.UserName select u;

                                if (exist.FirstOrDefault() == null)
                                {
                                    var result = UserManager.Create(user, "Password123@");
                                    if (result.Succeeded)
                                    {
                                        AspNetRole role = dc.AspNetRoles.Where(rl => rl.Name.Equals("STUDENT")).FirstOrDefault();
                                        Student student = new Student();
                                        AspNetUser aspNetUser = dc.AspNetUsers.Find(user.Id);
                                        aspNetUser.FirstName = FirstName;
                                        aspNetUser.LastName = LastName;
                                        aspNetUser.AspNetRoles.Add(role);
                                        student.UserId = aspNetUser.Id;
                                        student.Id_niv = Int32.Parse(niv);
                                        student.Id_fil = Int32.Parse(fil);
                                        student.Cne = cne;
                                        dc.Students.Add(student);
                                        dc.SaveChanges();
                                        ++addedStudent;
                                    }
                                    ViewBag.fileSize = result.ToString();
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