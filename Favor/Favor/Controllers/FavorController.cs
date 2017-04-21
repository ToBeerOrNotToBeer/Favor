using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Favor.Data;
using Microsoft.AspNet.Identity;
using Favor.Models.FavorModels;
using System.Data.Entity;
using Favor.GlobalConstants;

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
        public ActionResult Create(Data.Favor favor)
        {
            if (ModelState.IsValid)
            {
                var db = new FavorDbContext();

                var currentUserId = User.Identity.GetUserId();

                favor.UserId = currentUserId;

                var user = db.Users.Find(currentUserId);
                user.MyFavors.Add(favor);

                db.SaveChanges();
            }
            

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var fullFavor = db.Favors.Include(a => a.PayOff).FirstOrDefault(a => a.Id == id);

            if (fullFavor == null)
            {
                return RedirectToAction("Index", "Home");
            }

            FavorDetailViewModel favorDetailModel = new FavorDetailViewModel
            {
                Title = fullFavor.Title,
                Description = fullFavor.Description,
                CreationDate = fullFavor.CreationDate,
                UserEmail = fullFavor.User.Email,
                PayOff = fullFavor.PayOff,
                FavorType = fullFavor.FavorType
            };

            return View(favorDetailModel);
        }

        [Authorize]
        public ActionResult ListAllTypeSearch(int currentPage = 1)
        {
            var db = new FavorDbContext();

            var allTypeSearch = db
                                .Favors
                                .OrderBy(x => x.Id)
                                .Where(f => f.FavorType == FavorType.Offers)
                                .Skip((currentPage - 1) * PageConstants.CountOfFavorsOnPage)
                                .Take(PageConstants.CountOfFavorsOnPage)
                                .Select(f => new ListAllFavorsModel
                                {
                                    Id = f.Id,
                                    Title = f.Title,
                                    Description = f.Description,
                                    Category = f.FavorCategory,
                                    FullName = f.User.FullName
                                })
                                .ToList();

            if (allTypeSearch == null)
            {
                RedirectToAction("Index", "Home");
            }
            
            ViewBag.CurrentPage = currentPage;

            return View(allTypeSearch);
        }

        [Authorize]
        public ActionResult ListAllTypeDoing(int currentPage = 1)
        {
            var db = new FavorDbContext();

            var allTypeDoing = db
                                .Favors
                                .OrderBy(x => x.Id)
                                .Where(f => f.FavorType == FavorType.Seeks && !f.IsAccomplished)
                                .Skip((currentPage - 1) * PageConstants.CountOfFavorsOnPage)
                                .Take(PageConstants.CountOfFavorsOnPage)
                                .Select(f => new ListAllFavorsModel
                                {
                                    Id = f.Id,
                                    Title = f.Title,
                                    Description = f.Description,
                                    Category = f.FavorCategory,
                                    FullName = f.User.FullName
                                })
                                .ToList();

            if (allTypeDoing == null)
            {
                RedirectToAction("Index", "Home");
            }

            ViewBag.CurrentPage = currentPage;

            return View(allTypeDoing);
        }
    }
}