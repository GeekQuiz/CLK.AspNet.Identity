using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class PermissionManager<TRole, TPermission> : CLK.AspNet.Identity.PermissionManager<TPermission, string>
        where TRole : CLK.AspNet.Identity.EntityFramework.IdentityRole, new()
        where TPermission : CLK.AspNet.Identity.EntityFramework.IdentityPermission, new()
    {
        // Constructors
        public PermissionManager(DbContext context) : base(new CLK.AspNet.Identity.EntityFramework.PermissionStore<TRole, TPermission>(context)) { }
    }
}
