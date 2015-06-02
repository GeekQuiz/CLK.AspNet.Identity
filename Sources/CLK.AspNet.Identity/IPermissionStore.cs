using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public interface IPermissionStore<TPermission, in TKey> : IDisposable 
        where TPermission : class, IPermission<TKey>
    {
        // Methods
        Task CreateAsync(TPermission permission);
        
        Task UpdateAsync(TPermission permission);
        
        Task DeleteAsync(TPermission permission);
        
        Task<TPermission> FindByIdAsync(TKey permissionId);

        Task<TPermission> FindByNameAsync(string permissionName);
    }
}