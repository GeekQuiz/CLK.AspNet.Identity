using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CLK.AspNet.Identity.Mvc
{
    public abstract class RBACAuthorizeHelper
    {
        // Methods
        public bool HasPermission(IPrincipal user, string permissionName)
        {
            #region Contracts

            if (user == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(permissionName) == true) throw new ArgumentNullException();

            #endregion

            // PermissionAuthorize
            var permissionAuthorize = this.GetPermissionAuthorize();
            if (permissionAuthorize == null) throw new InvalidOperationException();

            // Authorize
            return permissionAuthorize.Authorize(user, permissionName);
        }

        protected abstract PermissionAuthorize GetPermissionAuthorize();
    }
}