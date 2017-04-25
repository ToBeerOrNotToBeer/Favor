using Favor.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Favor.Controllers
{
    public class AccomplishedFavorController : Controller
    {
        // GET: AccomplishedFavor
        public ActionResult AccomplishedFavorDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();
            var userId = this.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var accomplishedFavor = user.AccomplishedFavors.FirstOrDefault(f => f.Id == id);
            if (accomplishedFavor == null)
            {
                return RedirectToAction("Index", "Home");
            }



            return View(accomplishedFavor);
        }
    }
}