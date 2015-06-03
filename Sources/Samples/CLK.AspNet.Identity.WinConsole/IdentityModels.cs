using CLK.AspNet.Identity;
using CLK.AspNet.Identity.EntityFramework;
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
    // DbContext
    public class ApplicationDbContext : CLK.AspNet.Identity.EntityFramework.IdentityDbContext<ApplicationUser, ApplicationRole, ApplicationPermission>
    {
        // Constructors
        public ApplicationDbContext() : base("DefaultConnection") { }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
    }


    // Identity
    public class ApplicationUser : CLK.AspNet.Identity.EntityFramework.IdentityUser
    {
        // Constructors
        public ApplicationUser() : base() { }

        public ApplicationUser(string name) : base(name) { }
    }

    public class ApplicationRole : CLK.AspNet.Identity.EntityFramework.IdentityRole
    {
        // Constructors
        public ApplicationRole() : base() { }

        public ApplicationRole(string name) : base(name) { }
    }

    public class ApplicationPermission : CLK.AspNet.Identity.EntityFramework.IdentityPermission
    {
        // Constructors
        public ApplicationPermission() : base() { }

        public ApplicationPermission(string name) : base(name) { }
    }


    // Manager
    public class ApplicationUserManager : CLK.AspNet.Identity.EntityFramework.UserManager<ApplicationUser, ApplicationRole>
    {
        // Constuctors
        public ApplicationUserManager(ApplicationDbContext context) : base(context) { }
    }

    public class ApplicationRoleManager : CLK.AspNet.Identity.EntityFramework.RoleManager<ApplicationRole>
    {
        // Constuctors
        public ApplicationRoleManager(ApplicationDbContext context) : base(context) { }
    }

    public class ApplicationPermissionManager : CLK.AspNet.Identity.EntityFramework.PermissionManager<ApplicationRole, ApplicationPermission>
    {
        // Constuctors
        public ApplicationPermissionManager(ApplicationDbContext context) : base(context) { }
    }
}
