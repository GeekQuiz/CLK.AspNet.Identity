using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class RoleStore<TRole> : Microsoft.AspNet.Identity.EntityFramework.RoleStore<TRole, string, IdentityUserRole>
        where TRole : CLK.AspNet.Identity.EntityFramework.IdentityRole, new()
    {
        // Constructors
        public RoleStore(DbContext context) : base(context) { }
    }
}
