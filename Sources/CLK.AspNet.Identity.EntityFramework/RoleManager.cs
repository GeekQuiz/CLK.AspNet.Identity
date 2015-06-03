using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class RoleManager<TRole> : CLK.AspNet.Identity.RoleManager<TRole>
        where TRole : IdentityRole<string, IdentityUserRole, IdentityPermissionRole>, new()
    {
        // Constuctors
        public RoleManager(DbContext context) : base(new CLK.AspNet.Identity.EntityFramework.RoleStore<TRole>(context)) { }
    }
}
