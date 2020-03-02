using WebApp4CLKAspNetIdentityUsingJson.Models;
using CLK.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Security.Principal;

namespace WebApp4CLKAspNetIdentityUsingJson
{
    public class RBACAuthorizeHelper : CLK.AspNet.Identity.Mvc.RBACAuthorizeHelper
    {
        // Singleton
        private static RBACAuthorizeHelper _current = null;
        public static RBACAuthorizeHelper Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new RBACAuthorizeHelper();
                }
                return _current;
            }
        }


        // Methods
        protected override PermissionAuthorize GetPermissionAuthorize()
        {
            return new ApplicationPermissionAuthorize(HttpContext.Current.GetOwinContext().Get<ApplicationPermissionManager>());
        }
    }
}