using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using DailyTaskReport.Models;

namespace DailyTaskReport
{
    public partial class StartupAuth
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "KP8DTR",
                LoginPath = new PathString("/"),
                CookieName = "DTR",
                LogoutPath = new PathString("/"),
                ExpireTimeSpan = TimeSpan.FromMinutes(20)
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}