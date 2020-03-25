using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using realMiniProjet.Models.Entities;
using System.Data;
using System.Data.Entity;


namespace realMiniProjet.Controllers.Professor
{
    [Authorize(Roles ="PROFESSOR")]
    public class ProfessorController : Controller
    {
        // GET: Professor
        public ActionResult Index()
        {
            return View("profil");
        }

       


    }
}