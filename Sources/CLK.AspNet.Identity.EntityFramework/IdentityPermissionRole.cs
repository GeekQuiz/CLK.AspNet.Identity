using System;
using System.Collections.Generic;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class IdentityPermissionRole : IdentityPermissionRole<string>
    {

    }

    public class IdentityPermissionRole<TKey>
    {
        // Properties
        public virtual TKey PermissionId { get; set; }

        public virtual TKey RoleId { get; set; }
    }
}