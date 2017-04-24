using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Favor.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}