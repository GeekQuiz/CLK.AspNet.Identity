using WebApp4CLKAspNetIdentityUsingJson.Models;
using CLK.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;

namespace WebApp4CLKAspNetIdentityUsingJson
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RBACAuthorizeAttribute : CLK.AspNet.Identity.Mvc.RBACAuthorizeAttribute
    {
        // Methods
        protected override PermissionAuthorize GetPermissionAuthorize()
        {
            return new ApplicationPermissionAuthorize(HttpContext.Current.GetOwinContext().Get<ApplicationPermissionManager>());
        }
    }
}
