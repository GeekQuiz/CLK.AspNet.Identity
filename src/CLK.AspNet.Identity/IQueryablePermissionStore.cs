using System;
using System.Collections.Generic;
using System.Linq;

namespace CLK.AspNet.Identity
{
    public interface IQueryablePermissionStore<TPermission, in TKey> : IPermissionStore<TPermission, TKey> 
        where TPermission : class, IPermission<TKey>
    {
        // Properties
        IQueryable<TPermission> Permissions { get; }
    }
}