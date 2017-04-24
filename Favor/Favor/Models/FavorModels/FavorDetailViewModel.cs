
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
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public User User { get; set; }

        public string UserFullName { get; set; }

        public PayOff PayOff { get; set; }
        
        public FavorType FavorType { get; set; }

        public static string LastUsedUrl { get; set; }

        public bool IsAuthor(string name)
        {
            return this.User.UserName.Equals(name);
        }
    }
}