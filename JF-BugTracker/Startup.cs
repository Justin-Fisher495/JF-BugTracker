using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JF_BugTracker.Startup))]
namespace JF_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
