using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class PermissionAuthorize<TRole, TPermission> : CLK.AspNet.Identity.PermissionAuthorize<TPermission, string>
        where TRole : CLK.AspNet.Identity.EntityFramework.IdentityRole, new()
        where TPermission : CLK.AspNet.Identity.EntityFramework.IdentityPermission, new()
    {
        // Constructors
        public PermissionAuthorize(PermissionManager<TRole, TPermission> permissionManager) : base(permissionManager) { }
    }
}
