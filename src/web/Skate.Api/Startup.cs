﻿using Microsoft.Owin;
using Owin;
using TokenApp;

[assembly: OwinStartup(typeof(Skate.Api.Startup))]

namespace Skate.Api
{
    public class Startup
    {
        private readonly SecurityConfig service;

        public Startup()
        {
            service = new SecurityConfig();
        }

        public void Configuration(IAppBuilder app)
        {
            service.ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
