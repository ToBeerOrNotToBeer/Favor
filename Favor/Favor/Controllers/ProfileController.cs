using Favor.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Favor.Models.FavorModels;

namespace Favor.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Profile()
        {
            var db = new FavorDbContext();

            var currUserId = this.User.Identity.GetUserId();

            var userToView = db.Users.Include(u=> u.MyFavors).Include(u=> u.Messages).FirstOrDefault(u => u.Id == currUserId);

            if (userToView == null)
            {
                return RedirectToAction("Index", "Home");
            }

            FavorDetailViewModel.LastUsedUrl = "/Profile/Profile";

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