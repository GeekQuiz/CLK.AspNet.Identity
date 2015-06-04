using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.WinConsole
{
    // Context
    public class ApplicationDbContext : CLK.AspNet.Identity.EntityFramework.IdentityDbContext<ApplicationUser, ApplicationRole, ApplicationPermission>
    {
        // Constructors
        public ApplicationDbContext() : base("DefaultConnection") { }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
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
