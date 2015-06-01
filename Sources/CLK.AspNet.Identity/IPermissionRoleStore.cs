using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public interface IPermissionRoleStore<TPermission> : IPermissionRoleStore<TPermission, string> 
        where TPermission : class, IPermission<string>
    {

    }

    public interface IPermissionRoleStore<TPermission, in TKey> : IPermissionStore<TPermission, TKey> 
        where TPermission : class, IPermission<TKey>
    {
        // Methods
        Task AddToRoleAsync(TPermission permission, string roleName);

        Task RemoveFromRoleAsync(TPermission permission, string roleName);

        Task<IList<string>> GetRolesAsync(TPermission permission);
    }
}