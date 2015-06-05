using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public abstract class PermissionAuthorize
    {
        // Constructors
        internal PermissionAuthorize() { }


        // Methods
        public abstract bool Authorize(IPrincipal user, string permissionName);
    }

    public class PermissionAuthorize<TPermission, TKey> : PermissionAuthorize
        where TPermission : class, CLK.AspNet.Identity.IPermission<TKey>
        where TKey : IEquatable<TKey>
    {
        // Fields
        private PermissionManager<TPermission, TKey> _permissionManager = null;


        // Constructors
        public PermissionAuthorize(PermissionManager<TPermission, TKey> permissionManager)
        {
            #region Contracts

            if (permissionManager == null) throw new ArgumentNullException("permissionManager");

            #endregion

            // Default
            _permissionManager = permissionManager;
        }


        // Methods
        public override bool Authorize(IPrincipal user, string permissionName = null)
        {
            #region Contracts

            if (user == null) throw new ArgumentNullException("user");

            #endregion

            // Require
            if (user.Identity.IsAuthenticated == false) return false;
            if (string.IsNullOrEmpty(permissionName) == true) return true;

            // PermissionRoles
            var permissionRoles = _permissionManager.GetRolesByName(permissionName);
            if (permissionRoles == null) throw new InvalidOperationException();

            // Authorize
            if (permissionRoles.Count > 0 && permissionRoles.Any(user.IsInRole) == false)
            {
                return false;
            }
            if (permissionRoles.Count == 0 && string.IsNullOrEmpty(permissionName) == false) return false;

            // Return
            return true;
        }
    }
}
