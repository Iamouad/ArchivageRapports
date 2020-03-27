using Microsoft.AspNet.Identity;
using realMiniProjet.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realMiniProjet.Controllers.Etudiant
{
    [Authorize(Roles =("STUDENT"))]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }



    }
}