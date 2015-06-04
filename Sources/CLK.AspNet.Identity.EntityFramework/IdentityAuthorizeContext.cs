using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public abstract class IdentityAuthorizeContext<TUser, TRole, TPermission> : CLK.AspNet.Identity.AuthorizeContext<TUser, TRole, TPermission, string>
        where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, new()
        where TRole : IdentityRole<string, IdentityUserRole, IdentityPermissionRole>, new()
        where TPermission : IdentityPermission<string, IdentityPermissionRole>, new()
    {
        // Constuctors
        public IdentityAuthorizeContext() : base() { }
    }
}
