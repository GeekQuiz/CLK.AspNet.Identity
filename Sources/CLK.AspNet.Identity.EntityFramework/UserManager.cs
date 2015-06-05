using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class UserManager<TUser, TRole> : Microsoft.AspNet.Identity.UserManager<TUser, string>
        where TUser : CLK.AspNet.Identity.EntityFramework.IdentityUser, new()
        where TRole : CLK.AspNet.Identity.EntityFramework.IdentityRole, new()
    {
        // Constructors
        public UserManager(DbContext context) : base(new CLK.AspNet.Identity.EntityFramework.UserStore<TUser, TRole>(context)) { }
    }
}
