using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class RoleManager<TRole> : Microsoft.AspNet.Identity.RoleManager<TRole, string>
        where TRole : IdentityRole, new()
    {
        // Constructors
        public RoleManager(DbContext context) : base(new CLK.AspNet.Identity.EntityFramework.RoleStore<TRole>(context)) { }
    }
}
