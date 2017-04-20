
namespace Favor.Models.FavorModels
{
    using Favor.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// For the details view of the favor
    /// </summary>
    public class FavorDetailViewModel 
    {
        public DateTime CreationDate { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string UserEmail { get; set; }

        public PayOff PayOff { get; set; }
        
        public FavorType FavorType { get; set; }
    }
}