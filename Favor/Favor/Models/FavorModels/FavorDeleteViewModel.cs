using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Models.FavorModels
{
    public class FavorDeleteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public static string UrlOpenedFrom { get; set; }
    }
}