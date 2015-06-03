using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class UserManager<TUser, TRole> : CLK.AspNet.Identity.UserManager<TUser>
        where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, new()
        where TRole : IdentityRole<string, IdentityUserRole, IdentityPermissionRole>, new()
    {
        // Constuctors
        public UserManager(DbContext context) : base(new CLK.AspNet.Identity.EntityFramework.UserStore<TUser, TRole>(context)) { }
    }
}
