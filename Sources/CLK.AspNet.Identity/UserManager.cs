using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public class UserManager<TUser> : UserManager<TUser, string>
        where TUser : class, IUser<string>
    {
        // Constructors
        public UserManager(IUserStore<TUser, string> store) : base(store) { }
    }
}