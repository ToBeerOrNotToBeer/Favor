using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Favor.Data;
using Microsoft.AspNet.Identity;

namespace Favor.Controllers
{
    public class FavorController : Controller
    {
        // GET: Favor
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Data.Favor singleFavor)
        {
            var db = new FavorDbContext();
            //var user = db.Users.First(a => a.Id == User.Identity.GetUserId());

            singleFavor.UserId = User.Identity.GetUserId();

            return RedirectToAction("Index", "Home");
        }
    }
}