using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CLK.AspNet.Identity.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public abstract class RBACAuthorizeAttribute : AuthorizeAttribute
    {
        // Fields
        private string _permission = string.Empty;


        // Properties
        public string Permission
        {
            get { return _permission ?? String.Empty; }
            set { _permission = value; }
        }


        // Methods
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            #region Contracts

            if (filterContext == null) throw new ArgumentNullException();

            #endregion

            // HttpStatus - 403 Forbidden
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == true)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                return;
            }

            // Base
            base.HandleUnauthorizedRequest(filterContext);            
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            #region Contracts

            if (httpContext == null) throw new ArgumentNullException();

            #endregion

            // Base
            if (base.AuthorizeCore(httpContext) == false) return false;

            // PermissionAuthorize
            var permissionAuthorize = this.GetPermissionAuthorize();
            if (permissionAuthorize == null) throw new InvalidOperationException();

            // Authorize
            return permissionAuthorize.Authorize(httpContext.User, this.Permission);
        }

        protected abstract PermissionAuthorize GetPermissionAuthorize();
    }
}
