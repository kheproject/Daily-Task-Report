using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DailyTaskReport.Startup))]
namespace DailyTaskReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
