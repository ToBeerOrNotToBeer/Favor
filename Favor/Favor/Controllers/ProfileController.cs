﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Favor.Controllers
{
    public class ProfileController : Controller
    {
        
        [HttpGet]
        public ActionResult Profile()
        {
            return View();
        }
    }
}