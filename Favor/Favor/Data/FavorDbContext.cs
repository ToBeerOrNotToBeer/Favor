namespace Favor.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    
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
    }
}