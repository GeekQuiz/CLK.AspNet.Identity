using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        // Constructors
        public IdentityUser()
        {
            // Default
            this.Id = Guid.NewGuid().ToString();
            this.UserName = this.Id;
        }

        public IdentityUser(string name)
        {
            #region Contracts

            if (string.IsNullOrEmpty(name) == true) throw new ArgumentNullException("name");

            #endregion

            // Default
            this.Id = Guid.NewGuid().ToString();
            this.UserName = name;
        }
    }
}
