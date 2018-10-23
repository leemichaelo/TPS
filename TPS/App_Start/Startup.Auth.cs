using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TPS.Models;

namespace TPS
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/SignIn"),
                Provider = new CookieAuthenticationProvider()
            });
        }
    }
}