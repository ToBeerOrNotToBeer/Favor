using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Favor.Data;
using System.Data.Entity;
using Favor.Data;

namespace Favor.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult SearchResult(string toSearch, SearchOptions options)
        {
            if (toSearch == null || toSearch == string.Empty)
            {
                return RedirectToAction("Search");
            }

            var db = new FavorDbContext();
            

            if (options == SearchOptions.Favors)
            {
                SearchManager.Options = SearchOptions.Favors;

                SearchManager.FavorsToList = new List<Data.Favor>();

                SearchManager.FavorsToList = db.Favors.Where(f => f.Title.Contains(toSearch) ||
                                                            f.Description.Contains(toSearch) ||
                                                            f.User.FullName.Contains(toSearch))
                                                            .ToList();
            }
            else
            {
                SearchManager.Options = SearchOptions.Users;

                SearchManager.UsersToList = new List<Data.User>();

                SearchManager.UsersToList = db.Users.Where(u => u.FullName.Contains(toSearch) ||
                                                  u.Email.Contains(toSearch))
                                                  .Include(u => u.MyFavors)
                                                  .Include(u => u.AccomplishedFavors)
                                                  .ToList();
            }

            Favor.Models.FavorModels.FavorDetailViewModel.LastUsedUrl = "/Search/Search";

            return RedirectToAction("Search");
        }
    }
}