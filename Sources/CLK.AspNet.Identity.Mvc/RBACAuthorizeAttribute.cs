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
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            #region Contracts

            if (httpContext == null) throw new ArgumentNullException();

            #endregion

            // Base
            if (base.AuthorizeCore(httpContext) == false) return false;

            // PermissionAuthorize
            var permissionAuthorize = this.GetPermissionAuthorize(httpContext);
            if (permissionAuthorize == null) throw new InvalidOperationException();

            // Authorize
            return permissionAuthorize.Authorize(httpContext.User, this.Permission);
        }

        protected abstract PermissionAuthorize GetPermissionAuthorize(HttpContextBase httpContext);
    }
}
