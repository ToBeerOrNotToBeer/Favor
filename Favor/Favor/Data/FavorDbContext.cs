namespace Favor.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class FavorDbContext : IdentityDbContext<User>
    {
        public FavorDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FavorDbContext Create()
        {
            return new FavorDbContext();
        }

        public ICollection<Favor> AllFavors { get; set; }
    }
}