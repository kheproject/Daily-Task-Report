using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DailyTaskReport.StartupAuth))]
namespace DailyTaskReport
{
    public partial class StartupAuth
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}