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
}
