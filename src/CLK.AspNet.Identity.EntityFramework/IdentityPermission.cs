using System;
using System.Collections.Generic;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class IdentityPermission : CLK.AspNet.Identity.IPermission<string>
    {
        // Constructors
        public IdentityPermission()
        {
            // Default
            this.Id = Guid.NewGuid().ToString();
            this.Name = this.Id;
            this.Roles = new List<IdentityPermissionRole>();
        }

        public IdentityPermission(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException("name");

            #endregion

            // Default
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Roles = new List<IdentityPermissionRole>();
        }


        // Properties
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<IdentityPermissionRole> Roles { get; private set; }        
    }
}