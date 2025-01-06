using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shop.WebUI.Tests.Startup))]
namespace Shop.WebUI.Tests
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
