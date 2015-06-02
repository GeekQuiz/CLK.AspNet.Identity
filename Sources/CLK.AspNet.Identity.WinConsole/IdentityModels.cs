using CLK.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.WinConsole
{
    // Identity
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }

        public ApplicationUser(string name) : base(name) { }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name) : base(name) { }
    }


    // Manager
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        // Constuctors
        public ApplicationUserManager(ApplicationDbContext context) : this(new UserStore<ApplicationUser>(context)) { }

        public ApplicationUserManager(UserStore<ApplicationUser> store) : base(store) { }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        // Constuctors
        public ApplicationRoleManager(ApplicationDbContext context) : this(new RoleStore<ApplicationRole>(context)) { }

        public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store) { }
    }


    // DbContext
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole>
    {
        // Constructors
        public ApplicationDbContext() : base("DefaultConnection") { }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }


        // Properties
        new public virtual IDbSet<ApplicationRole> Roles { get; set; }
    }
}
