using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CLK.AspNet.Identity.WebSite.Startup))]
namespace CLK.AspNet.Identity.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
