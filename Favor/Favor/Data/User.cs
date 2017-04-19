namespace Favor.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public User()
        {
            this.MyFavors = new List<string>();
            this.PendingFavors = new List<string>();
            this.AccomplishedFavors = new List<string>();
            this.SentFavors = new List<string>();
        }

        public ICollection<string> MyFavors { get; set; }

        public ICollection<string> PendingFavors { get; set; }

        public ICollection<string> AccomplishedFavors { get; set; }

        public ICollection<string> SentFavors { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}