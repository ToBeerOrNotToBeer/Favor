﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Favor.Startup))]
namespace Favor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
