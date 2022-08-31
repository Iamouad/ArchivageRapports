﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realMiniProjet.Controllers
{
    [Authorize(Roles ="ADMIN")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("Profil");
        }
    }
}