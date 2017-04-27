using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public static class SearchManager
    {
        public static SearchOptions Options { get; set; }

        public static List<User> UsersToList { get; set; }

        public static List<Favor> FavorsToList { get; set; }
    }
}