using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class IdentityRole : Microsoft.AspNet.Identity.EntityFramework.IdentityRole<string, IdentityUserRole>
    {
        // Constructors
        public IdentityRole()
        {
            // Default
            this.Id = Guid.NewGuid().ToString();
            this.Name = this.Id;
            this.Permissions = new List<IdentityPermissionRole>();
        }

        public IdentityRole(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException("name");

            #endregion

            // Default
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Permissions = new List<IdentityPermissionRole>();
        }


        // Properties
        public virtual ICollection<IdentityPermissionRole> Permissions { get; private set; }
    }
}
