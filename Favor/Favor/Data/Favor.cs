namespace Favor.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class Favor
    {
        public Favor()
        {
            CreationDate = DateTime.Now;
           
        }

        public int Id { get; set; }

        public DateTime CreationDate { get; }      

        [Required]
        [MaxLength(150, ErrorMessage = "The title is too long!")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string UserId { get; set; }   
        
        public virtual User User { get; set; }
        
        [Required]
        public PayOff PayOff { get; set; }

        [Required]
        [DisplayName("Favor Type")]
        public FavorType FavorType { get; set; }

        [Required]
        [DisplayName("Favor Category")]
        public Category FavorCategory { get; set; }

    }
}