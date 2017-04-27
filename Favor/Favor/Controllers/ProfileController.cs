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

            var userToView = db.Users.Include(u=> u.MyFavors)
                .Include(u=> u.Messages)
                .Include(u=> u.SentFavors)
                .Include(u=> u.PendingFavors)
                .Include(u=> u.AccomplishedFavors)
                .Include(u=> u.TicketsForSender)
                .Include(u=> u.TicketsForAdmin)
                .FirstOrDefault(u => u.Id == currUserId);

            if (userToView == null)
            {
                return RedirectToAction("Index", "Home");
            }

            FavorDetailViewModel.LastUsedUrl = "/Profile/Profile";
            FavorDeleteViewModel.UrlOpenedFrom = "/Profile/Profile";
            FavorEditViewModel.UrlOpenedFrom = "/Profile/Profile";

            return View(userToView);
        }
       

        [HttpGet]
        [Authorize]
        public ActionResult OtherProfile(string otherProfileId)
        {
            if (otherProfileId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var otherUser = db.Users
                .Include(u=> u.MyFavors)
                .FirstOrDefault(u=> u.Id == otherProfileId);

            if (otherUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(otherUser);
        }
        
        [Authorize]
        public ActionResult DeleteAllMessages(int? profileIntegerId)
        {
            if (profileIntegerId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var user = db.Users
                .Include(u => u.Messages)
                .Include(u => u.TicketsForSender)
                .Include(u => u.TicketsForAdmin)
                .FirstOrDefault(u => u.IntegerId == profileIntegerId);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            user.Messages.Clear();
            user.TicketsForAdmin.Clear();
            user.TicketsForSender.Clear();

            db.SaveChanges();

            return RedirectToAction("Profile", "Profile");
        }
    }
}