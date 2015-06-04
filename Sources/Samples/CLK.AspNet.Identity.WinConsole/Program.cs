using CLK.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.WinConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var datebaseContext = CreateDatabaseContext<ApplicationDbContext>())
            {
                // Const                
                const string userName = "user001";
                const string password = "P@ssw0rd";
                const string email = "user001@hotmail.com";
                const string roleName = "admin";
                const string permissionName = "write";

                // Manager
                var userManager = new ApplicationUserManager(datebaseContext);
                var roleManager = new ApplicationRoleManager(datebaseContext);
                var permissionManager = new ApplicationPermissionManager(datebaseContext);

                // User
                var user = userManager.FindByName(userName);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = userName, Email = email };
                    userManager.Create(user, password);
                    userManager.SetLockoutEnabled(user.Id, false);
                }

                // Role
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole(roleName);
                    roleManager.Create(role);
                }                

                // Permission
                var permission = permissionManager.FindByName(permissionName);
                if (permission == null)
                {
                    permission = new ApplicationPermission(permissionName);
                    permissionManager.Create(permission);
                }

                // UserRole
                var userRoleNames = userManager.GetRoles(user.Id);
                if (userRoleNames.Contains(role.Name) == false)
                {
                    userManager.AddToRole(user.Id, role.Name);
                }

                // PermissionRole
                var permissionRoleNames = permissionManager.GetRolesById(permission.Id);
                if (permissionRoleNames.Contains(role.Name) == false)
                {
                    permissionManager.AddToRole(permission.Id, role.Name);
                }
            }

            Console.Write("End...");
            Console.ReadLine();
        }

        static TDatabaseContext CreateDatabaseContext<TDatabaseContext>()
            where TDatabaseContext : DbContext, new()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TDatabaseContext>());
            var db = new TDatabaseContext();
            db.Database.Initialize(true);
            return db;
        }
    }
}
