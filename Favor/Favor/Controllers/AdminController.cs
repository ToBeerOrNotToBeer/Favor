using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Favor.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult AdminPanel()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new Favor.Data.FavorDbContext();

            var allUsers = db.Users.ToList();

            return View(allUsers);
        }
    }
}