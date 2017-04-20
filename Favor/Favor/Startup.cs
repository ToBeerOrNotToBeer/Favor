using Favor.Data;
using Favor.Migrations;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(Favor.Startup))]
namespace Favor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<FavorDbContext, Configuration>());

            ConfigureAuth(app);
        }
    }
}
