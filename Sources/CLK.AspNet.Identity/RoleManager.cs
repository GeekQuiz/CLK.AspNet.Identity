using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public class RoleManager<TRole> : RoleManager<TRole, string>
        where TRole : class, IRole<string>
    {
        // Constructors
        public RoleManager(IRoleStore<TRole, string> store) : base(store) { }
    }
}