﻿using System;
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
                Id = fullFavor.Id,
                Title = fullFavor.Title,
                Description = fullFavor.Description,
                CreationDate = fullFavor.CreationDate,
                UserFullName = fullFavor.User.FullName,
                PayOff = fullFavor.PayOff,
                FavorType = fullFavor.FavorType,
                User = fullFavor.User
            };

            FavorDeleteViewModel.UrlOpenedFrom = $"/Favor/Details/{id}";
            FavorEditViewModel.UrlOpenedFrom = $"/Favor/Details/{id}";

            return View(favorDetailModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var fullFavor = db.Favors.Include(f => f.PayOff).FirstOrDefault(f => f.Id == id);

            if (fullFavor == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var favorEditViewModel = new FavorEditViewModel
            {
                Id = fullFavor.Id,
                Title = fullFavor.Title,
                Description = fullFavor.Description,
                PayOff = fullFavor.PayOff,
                FavorType = fullFavor.FavorType,
                Category = fullFavor.FavorCategory
            };

            return View(favorEditViewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(FavorEditViewModel favorEditViewModel)
        {
            if (favorEditViewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var fullFavor = db.Favors.FirstOrDefault(f => f.Id == favorEditViewModel.Id);

            if (fullFavor == null)
            {
                return RedirectToAction("Index", "Home");
            }

            fullFavor.Title = favorEditViewModel.Title;
            fullFavor.Description = favorEditViewModel.Description;
            fullFavor.PayOff = favorEditViewModel.PayOff;
            fullFavor.FavorType = favorEditViewModel.FavorType;
            fullFavor.FavorCategory = favorEditViewModel.Category;

            db.Entry(fullFavor).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { @id = fullFavor.Id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var fullFavor = db.Favors.FirstOrDefault(f => f.Id == id);

            if (fullFavor == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var favorDeleteModel = new FavorDeleteViewModel
            {
                Title = fullFavor.Title,
                Description = fullFavor.Description,
                Id = fullFavor.Id
            };

            return View(favorDeleteModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(FavorDeleteViewModel favorDeleteViewModel)
        {
            if (favorDeleteViewModel ==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();
            var fullFavor = db.Favors.FirstOrDefault(f => f.Id == favorDeleteViewModel.Id);
            if (fullFavor ==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var allUsers = db.Users.Include(u => u.SentFavors).Include(u => u.PendingFavors).Include(u => u.Messages).ToList();

            DeleteAllPendingAndSentRequestsFromUsersAccounts(allUsers, favorDeleteViewModel.Id);
            
            db.Favors.Remove(fullFavor);

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private void DeleteAllPendingAndSentRequestsFromUsersAccounts(List<User> allUsers, int idToDelete)
        {
            for (int currUser = 0; currUser < allUsers.Count; currUser++)
            {
                var user = allUsers[currUser];

                if (user.SentFavors.Any(f => f.FavorId == idToDelete))
                {
                    user.SentFavors.RemoveAll(f => f.FavorId == idToDelete);
                }

                if (user.PendingFavors.Any(f => f.FavorId == idToDelete))
                {
                    user.PendingFavors.RemoveAll(f => f.FavorId == idToDelete);
                }
            }
        }

        [Authorize]
        public ActionResult ListAllTypeSearch(int currentPage = 1, Category category = (Category)0)
        {
            var db = new FavorDbContext();

            List<ListAllFavorsModel> allTypeSearch = new List<ListAllFavorsModel>();
            if (category == Category.All)
            {
                allTypeSearch = db
                                .Favors
                                .OrderByDescending(x => x.Id)
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
            }
            else
            {
                allTypeSearch = db
                                .Favors
                                .OrderByDescending(x => x.Id)
                                .Where(f => f.FavorType == FavorType.Offers &&
                                                f.FavorCategory == category)
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
            }
            

            if (allTypeSearch == null)
            {
                RedirectToAction("Index", "Home");
            }
            
            ViewBag.CurrentPage = currentPage;
            ViewBag.CurrentCategory = category;

            FavorDetailViewModel.LastUsedUrl = $"/Favor/ListAllTypeSearch?currentPage={currentPage}&category={category}";

            return View(allTypeSearch);
        }

        [Authorize]
        public ActionResult ListAllTypeDoing(int currentPage = 1, Category category = (Category)0)
        {
            var db = new FavorDbContext();

            List<ListAllFavorsModel> allTypeDoing = new List<ListAllFavorsModel>();
            if (category == Category.All)
            {
                allTypeDoing = db
                                .Favors
                                .OrderByDescending(x => x.Id)
                                .Where(f => f.FavorType == FavorType.Seeks)
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
            }
            else
            {
                allTypeDoing = db
                                .Favors
                                .OrderByDescending(x => x.Id)
                                .Where(f => f.FavorType == FavorType.Seeks
                                                         && f.FavorCategory == category)
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
            }

            if (allTypeDoing == null)
            {
                RedirectToAction("Index", "Home");
            }

            ViewBag.CurrentPage = currentPage;
            ViewBag.CurrentCategory = category;

            FavorDetailViewModel.LastUsedUrl = $"/Favor/ListAllTypeDoing?currentPage={currentPage}&category={category}";

            return View(allTypeDoing);
        }


        public bool IsUserAuthorizedToEdit(FavorDetailViewModel favor)
        {
            var isAdmin = this.User.IsInRole("Admin");
            var isAuthor = favor.IsAuthor(this.User.Identity.Name);

            return isAdmin || isAuthor;

        }
    }
}