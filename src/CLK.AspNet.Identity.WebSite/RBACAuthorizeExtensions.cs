﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CLK.AspNet.Identity.WebSite
{
    public static class RBACAuthorizeExtensions
    {
        // Methods
        public static bool HasPermission(this IPrincipal user, string permission)
        {
            #region Contracts

            if (user == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(permission) == true) throw new ArgumentNullException();

            #endregion

            // HasPermission
            return RBACAuthorizeHelper.Current.HasPermission(user, permission);
        }
    }
}