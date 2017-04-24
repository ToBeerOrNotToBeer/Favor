using Favor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Models.FavorModels
{
    public class FavorEditViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public PayOff PayOff { get; set; }

        public FavorType FavorType { get; set; }

        public Category Category { get; set; }

        public static string UrlOpenedFrom { get; set; }
    }
}