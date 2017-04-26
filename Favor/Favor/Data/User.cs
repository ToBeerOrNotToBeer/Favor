namespace Favor.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using Models.FavorModels;
    using System.ComponentModel.DataAnnotations;
    using System;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public User()
        {
            this.MyFavors = new List<Favor>();
            this.PendingFavors = new List<FavorTradeModel>();
            this.AccomplishedFavors = new List<AccomplishedTradeModel>();
            this.SentFavors = new List<FavorTradeModel>();
            this.Messages = new List<Message>();
            this.TicketsForSender = new List<TicketForSender>();
            this.TicketsForAdmin = new List<TicketForAdmin>();
        }

        [Required]
        public string FullName { get; set; }

        public List<Favor> MyFavors { get; set; }

        public List<FavorTradeModel> PendingFavors { get; set; }

        public List<AccomplishedTradeModel> AccomplishedFavors { get; set; }

        public List<FavorTradeModel> SentFavors { get; set; }

        public List<Message> Messages { get; set; }

        public List<TicketForSender> TicketsForSender { get; set; }

        public List<TicketForAdmin> TicketsForAdmin { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}