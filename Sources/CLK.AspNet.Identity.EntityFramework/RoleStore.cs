using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class RoleStore<TRole> : RoleStore<TRole, string, IdentityUserRole>
        where TRole : IdentityRole<string, IdentityUserRole, IdentityPermissionRole>, new()
    {
        // Constructors
        public RoleStore(DbContext context) : base(context) { }
    }
}
