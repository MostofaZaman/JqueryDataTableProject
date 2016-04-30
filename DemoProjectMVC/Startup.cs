using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoProjectMVC.Startup))]
namespace DemoProjectMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
