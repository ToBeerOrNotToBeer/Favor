using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Models.SearchModels
{
    public class SearchModel
    {
        public string ToSearch { get; set; }

        public SearchType Type { get; set; }
    }
}