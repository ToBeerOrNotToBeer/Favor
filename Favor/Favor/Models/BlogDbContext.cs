namespace Favor.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class FavorDbContext : IdentityDbContext<ApplicationUser>
    {
        public FavorDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FavorDbContext Create()
        {
            return new FavorDbContext();
        }
    }
}