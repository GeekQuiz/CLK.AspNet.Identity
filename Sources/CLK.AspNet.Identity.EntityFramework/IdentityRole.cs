using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
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
