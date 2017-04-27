using System;
using System.Collections.Generic;
using System.Linq;
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
            //if (!this.User.IsInRole("Admin"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var db = new Favor.Data.FavorDbContext();

            var allUsers = db.Users.ToList();

            return View(allUsers);
        }

        [Authorize]
        public ActionResult DeleteUser(int? integerIdToDelete)
        {
            if (integerIdToDelete == null)
            {
                return RedirectToAction("AdminPanel");
            }

            var db = new Favor.Data.FavorDbContext();

            var userToRemove = db.Users.FirstOrDefault(u => u.IntegerId == integerIdToDelete);

            if (userToRemove == null)
            {
                return RedirectToAction("AdminPanel");
            }

            db.Users.Remove(userToRemove);

            db.SaveChanges();

            return RedirectToAction("AdminPanel");
        }
    }
}