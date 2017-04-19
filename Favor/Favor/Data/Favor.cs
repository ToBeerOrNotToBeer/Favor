﻿namespace Favor.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    public class Favor
    { 
        public int Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "The title is too long!")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }


    }
}