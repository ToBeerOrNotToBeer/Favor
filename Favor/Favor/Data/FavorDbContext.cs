namespace Favor.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class FavorDbContext : IdentityDbContext<User>
    {
        public FavorDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public IDbSet<Favor> Favors { get; set; }

        public static FavorDbContext Create()
        {
            return new FavorDbContext();
        }
    }
}