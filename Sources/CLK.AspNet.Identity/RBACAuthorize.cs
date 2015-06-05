using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public abstract class RBACAuthorize
    {
        // Fields
        private string _permissionName = string.Empty;


        // Constructors
        internal RBACAuthorize() { }


        // Properties
        public string PermissionName
        {
            get { return _permissionName ?? String.Empty; }
            set { _permissionName = value; }
        }


        // Methods
        public abstract bool Authorize(IPrincipal user);
    }

    public sealed class RBACAuthorize<TPermission, TKey> : RBACAuthorize
        where TPermission : class, CLK.AspNet.Identity.IPermission<TKey>
        where TKey : IEquatable<TKey>
    {
        // Fields
        private Func<PermissionManager<TPermission, TKey>> _getPermissionManagerDelegate = null;


        // Constructors
        internal RBACAuthorize(Func<PermissionManager<TPermission, TKey>> getPermissionManagerDelegate)
        {
            #region Contracts

            if (getPermissionManagerDelegate == null) throw new ArgumentNullException("getPermissionManagerDelegate");

            #endregion

            // Default
            _getPermissionManagerDelegate = getPermissionManagerDelegate;
        }


        // Methods
        public override bool Authorize(IPrincipal user)
        {
            #region Contracts

            if (user == null) throw new ArgumentNullException("user");

            #endregion

            // Require
            if (user.Identity.IsAuthenticated == false) return false;

            // PermissionManager
            var permissionManager = _getPermissionManagerDelegate();
            if (permissionManager == null) throw new InvalidOperationException();

            // PermissionRoles
            var permissionRoles = permissionManager.GetRolesByName(this.PermissionName);
            if (permissionRoles == null) throw new InvalidOperationException();

            // Authorize
            if (permissionRoles.Count > 0 && permissionRoles.Any(user.IsInRole) == false)
            {
                return false;
            }
            if (permissionRoles.Count == 0 && string.IsNullOrEmpty(this.PermissionName) == false) return false;

            // Return
            return true;
        }
    }
}
