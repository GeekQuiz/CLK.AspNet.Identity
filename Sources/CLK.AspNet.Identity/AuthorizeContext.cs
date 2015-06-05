using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public abstract class AuthorizeContext<TUser, TRole, TPermission, TKey>
       where TUser : class, Microsoft.AspNet.Identity.IUser<TKey>
       where TRole : class, Microsoft.AspNet.Identity.IRole<TKey>
       where TPermission : class, CLK.AspNet.Identity.IPermission<TKey>
       where TKey : IEquatable<TKey>
    {
        // Properties
        protected abstract PermissionManager<TPermission, TKey> PermissionManager { get; }


        // Methods       
        public RBACAuthorize CreateAuthorize()
        {
            // Return
            return new RBACAuthorize<TPermission, TKey>(() => this.PermissionManager);
        }
    }
}
