﻿using Favor.Data;
using Microsoft.AspNet.Identity;
using System;
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
            var db = new FavorDbContext();

            var currUserId = this.User.Identity.GetUserId();

            var userToView = db.Users.FirstOrDefault(u => u.Id == currUserId);

            if (userToView == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(userToView);
        }
    }
}