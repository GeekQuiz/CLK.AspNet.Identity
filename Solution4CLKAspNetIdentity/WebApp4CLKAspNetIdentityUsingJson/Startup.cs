using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp4CLKAspNetIdentityUsingJson.Startup))]
namespace WebApp4CLKAspNetIdentityUsingJson
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
