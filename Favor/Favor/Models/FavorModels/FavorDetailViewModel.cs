
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
        public DateTime CreationDate { get; }

        [Required]
        [MaxLength(150, ErrorMessage = "The title is too long!")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int UserId { get; set; }

        [Required]
        public PayOff PayOff { get; set; }
    }
}