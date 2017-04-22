using Favor.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var db = new FavorDbContext();

            var currUserId = this.User.Identity.GetUserId();

            var userToView = db.Users.Include(u=> u.MyFavors).FirstOrDefault(u => u.Id == currUserId);

            if (userToView == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(userToView);
        }
       

        [HttpGet]
        public ActionResult OtherProfile(string otherProfileId)
        {
            if (otherProfileId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var otherUser = db.Users.Find(otherProfileId);

            if (otherUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(otherUser);
        }
    }
}