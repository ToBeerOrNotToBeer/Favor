using Favor.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult SearchResult(SearchModel model)
        {
            return RedirectToAction("Search");
        }
    }
}