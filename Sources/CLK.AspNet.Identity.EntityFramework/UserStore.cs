using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class UserStore<TUser, TRole> : UserStore<TUser, TRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, new()
        where TRole : IdentityRole<string, IdentityUserRole, IdentityPermissionRole>, new()
    {
        // Constructors
        public UserStore(DbContext context) : base(context) { }
    }
}
