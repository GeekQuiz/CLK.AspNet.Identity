using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class IdentityRole : IdentityRole<string, IdentityUserRole, IdentityPermissionRole>, IPermission
    {
        // Constructors
        public IdentityRole()
        {
            // Default
            this.Id = Guid.NewGuid().ToString();
            this.Name = this.Id;
        }

        public IdentityRole(string roleName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(roleName) == true) throw new ArgumentNullException("roleName");

            #endregion

            // Default
            this.Id = Guid.NewGuid().ToString();
            this.Name = roleName;
        }
    }

    public class IdentityRole<TKey, TUserRole, TPermissionRole> : IdentityRole<TKey, TUserRole>
        where TUserRole : IdentityUserRole<TKey>
        where TPermissionRole : IdentityPermissionRole<TKey>
    {
        // Constructor
        public IdentityRole()
        {
            // Default
            this.Permissions = new List<TPermissionRole>();
        }


        // Properties
        public virtual ICollection<TPermissionRole> Permissions { get; private set; }
    }
}
