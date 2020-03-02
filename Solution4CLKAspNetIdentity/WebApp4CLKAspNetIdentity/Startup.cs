using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp4CLKAspNetIdentity.Startup))]
namespace WebApp4CLKAspNetIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
