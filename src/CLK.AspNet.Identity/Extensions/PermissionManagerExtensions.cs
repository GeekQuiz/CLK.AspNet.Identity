using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public static class PermissionManagerExtensions
    {
        // Methods
        public static IdentityResult Create<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TPermission permission)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.CreateAsync(permission));
        }

        public static IdentityResult Update<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TPermission permission)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.UpdateAsync(permission));
        }

        public static IdentityResult Delete<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TPermission permission)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.DeleteAsync(permission));
        }

        public static TPermission FindById<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TKey permissionId)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.FindByIdAsync(permissionId));
        }

        public static TPermission FindByName<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, string permissionName)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.FindByNameAsync(permissionName));
        }


        public static IdentityResult AddToRole<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TKey permissionId, string role)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.AddToRoleAsync(permissionId, role));
        }

        public static IdentityResult AddToRoles<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TKey permissionId, params string[] roles)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.AddToRolesAsync(permissionId, roles));
        }

        public static IdentityResult RemoveFromRole<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TKey permissionId, string role)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.RemoveFromRoleAsync(permissionId, role));
        }
        
        public static IdentityResult RemoveFromRoles<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TKey permissionId, params string[] roles)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.RemoveFromRolesAsync(permissionId, roles));
        }

        public static IList<string> GetRolesById<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, TKey permissionId)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetRolesByIdAsync(permissionId));
        }

        public static IList<string> GetRolesByName<TPermission, TKey>(this PermissionManager<TPermission, TKey> manager, string permissionName)
            where TKey : IEquatable<TKey>
            where TPermission : class, IPermission<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync(() => manager.GetRolesByNameAsync(permissionName));
        }
    }
}
