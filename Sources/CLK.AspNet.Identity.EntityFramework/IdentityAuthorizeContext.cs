using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public abstract class IdentityAuthorizeContext<TUser, TRole, TPermission> : CLK.AspNet.Identity.AuthorizeContext<TUser, TRole, TPermission, string>
        where TUser : IdentityUser, new()
        where TRole : IdentityRole, new()
        where TPermission : IdentityPermission, new()
    {
        // Constructors
        public IdentityAuthorizeContext() : base() { }
    }
}
