using CLK.AspNet.Identity;
using CLK.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebApp4CLKAspNetIdentityUsingJson.Models
{
    // Context
    public partial class ApplicationDbContext
    {
        // Constructors
        static ApplicationDbContext()
        {
            // Database
            Database.SetInitializer(new DropCreateForModelChangesDbInitializer<ApplicationDbContext>(Initialize));
        }


        // Methods
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("DefaultConnection");
        }

        public static void Initialize(ApplicationDbContext context)
        {
            #region Contracts

            if (context == null)
                throw new ArgumentNullException();

            #endregion

            #region 產生人員權限管理物件(Manager)
            #region 使用者物件
            ApplicationUserManager userManager = ApplicationUserManager.Create(context);
            #endregion
            #region 角色物件
            ApplicationRoleManager roleManager = ApplicationRoleManager.Create(context);
            #endregion
            #region 讀取權限物件
            ApplicationPermissionManager permissionManager = ApplicationPermissionManager.Create(context);
            #endregion
            #endregion

            #region 初始化人員權限管理(Initialize)
            try
            {
                #region 預設的使用者(Default - User)
                const string adminUserName = "admin@example.com";
                const string adminUserPassword = "admin123";

                const string guestUserName = "guest@example.com";
                const string guestUserPassword = "guest123";
                #endregion
                #region 預設的角色(Default - Role)
                const string adminRoleName = "Admin";
                const string guestRoleName = "Guest";
                #endregion
                #region 預設的讀取權限(Default - Permission)
                const string accessPermissionName = "AccessAccess";
                const string contactPermissionName = "ContactAccess";
                const string productAddPermissionName = "ProductAddAccess";
                const string productRemovePermissionName = "ProductRemoveAccess";
                #endregion
                #region 新增預設的使用者(Setup Default - User)
                var adminUser = userManager.FindByName(adminUserName);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
                    userManager.Create(adminUser, adminUserPassword);
                    userManager.SetLockoutEnabled(adminUser.Id, false);
                }

                var guestUser = userManager.FindByName(guestUserName);
                if (guestUser == null)
                {
                    guestUser = new ApplicationUser { UserName = guestUserName, Email = guestUserName };
                    userManager.Create(guestUser, guestUserPassword);
                    userManager.SetLockoutEnabled(guestUser.Id, false);
                }
                #endregion
                #region 新增預設的角色(Setup Default - Role)
                var adminRole = roleManager.FindByName(adminRoleName);
                if (adminRole == null)
                {
                    adminRole = new ApplicationRole(adminRoleName);
                    roleManager.Create(adminRole);
                }

                var guestRole = roleManager.FindByName(guestRoleName);
                if (guestRole == null)
                {
                    guestRole = new ApplicationRole(guestRoleName);
                    roleManager.Create(guestRole);
                }
                #endregion
                #region 新增預設的讀取權限(Setup Default - Permission)
                var accessPermission = permissionManager.FindByName(accessPermissionName);
                if (accessPermission == null)
                {
                    accessPermission = new ApplicationPermission(accessPermissionName);
                    permissionManager.Create(accessPermission);
                }

                var contactPermission = permissionManager.FindByName(contactPermissionName);
                if (contactPermission == null)
                {
                    contactPermission = new ApplicationPermission(contactPermissionName);
                    permissionManager.Create(contactPermission);
                }

                var productAddPermission = permissionManager.FindByName(productAddPermissionName);
                if (productAddPermission == null)
                {
                    productAddPermission = new ApplicationPermission(productAddPermissionName);
                    permissionManager.Create(productAddPermission);
                }

                var productRemovePermission = permissionManager.FindByName(productRemovePermissionName);
                if (productRemovePermission == null)
                {
                    productRemovePermission = new ApplicationPermission(productRemovePermissionName);
                    permissionManager.Create(productRemovePermission);
                }
                #endregion
                #region 導入角色給預設的使用者(UserAddToRole)
                IList<string> rolesForUser = null;

                rolesForUser = userManager.GetRoles(adminUser.Id);
                if (rolesForUser.Contains(adminRole.Name) == false)
                {
                    userManager.AddToRole(adminUser.Id, adminRole.Name);
                }

                rolesForUser = userManager.GetRoles(guestUser.Id);
                if (rolesForUser.Contains(guestRole.Name) == false)
                {
                    userManager.AddToRole(guestUser.Id, guestRole.Name);
                }
                #endregion
                #region 導入讀取權限給角色(PermissionAddToRole)
                IList<string> rolesForPermission = null;

                rolesForPermission = permissionManager.GetRolesById(accessPermission.Id);
                if (rolesForPermission.Contains(adminRole.Name) == false)
                {
                    permissionManager.AddToRole(accessPermission.Id, adminRole.Name);
                }

                rolesForPermission = permissionManager.GetRolesById(contactPermission.Id);
                if (rolesForPermission.Contains(adminRole.Name) == false)
                {
                    permissionManager.AddToRole(contactPermission.Id, adminRole.Name);
                }

                rolesForPermission = permissionManager.GetRolesById(productAddPermission.Id);
                if (rolesForPermission.Contains(adminRole.Name) == false)
                {
                    permissionManager.AddToRole(productAddPermission.Id, adminRole.Name);
                }

                rolesForPermission = permissionManager.GetRolesById(productRemovePermission.Id);
                if (rolesForPermission.Contains(adminRole.Name) == false)
                {
                    permissionManager.AddToRole(productRemovePermission.Id, adminRole.Name);
                }
                #endregion
            }
            finally
            {
                // Dispose
                userManager.Dispose();
                roleManager.Dispose();
                permissionManager.Dispose();
            }
            #endregion
        }
    }


    // Manager
    public partial class ApplicationUserManager
    {
        // Methods
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            return ApplicationUserManager.Create(context.Get<ApplicationDbContext>(), options.DataProtectionProvider);
        }

        public static ApplicationUserManager Create(ApplicationDbContext context, IDataProtectionProvider dataProtectionProvider = null)
        {
            #region Contracts

            if (context == null) throw new ArgumentNullException();

            #endregion

            // 建立使用者管理員
            var userManager = new ApplicationUserManager(context);
            if (userManager == null) throw new InvalidOperationException();

            // 設定使用者名稱的驗證邏輯
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 設定密碼的驗證邏輯
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,              // 最小長度
                RequireNonLetterOrDigit = false, // 是否需要一個非字母或是數字
                RequireDigit = false,            // 是否需要一個數字
                RequireLowercase = false,        // 是否需要一個小寫字母
                RequireUppercase = false,        // 是否需要一個大寫字母
            };

            // 設定使用者鎖定詳細資料
            userManager.UserLockoutEnabledByDefault = true;
            userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            userManager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 註冊雙因素驗證提供者。此應用程式使用手機和電子郵件接收驗證碼以驗證使用者
            // 您可以撰寫專屬提供者，並將它外掛到這裡。
            userManager.RegisterTwoFactorProvider("電話代碼", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "您的安全碼為 {0}"
            });
            userManager.RegisterTwoFactorProvider("電子郵件代碼", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "安全碼",
                BodyFormat = "您的安全碼為 {0}"
            });
            userManager.EmailService = new EmailService();
            userManager.SmsService = new SmsService();
            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            // 回傳
            return userManager;
        }
    }

    public partial class ApplicationRoleManager
    {
        // Methods
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return ApplicationRoleManager.Create(context.Get<ApplicationDbContext>());
        }

        public static ApplicationRoleManager Create(ApplicationDbContext context)
        {
            return new ApplicationRoleManager(context);
        }
    }

    public partial class ApplicationPermissionManager
    {
        // Methods
        public static ApplicationPermissionManager Create(IdentityFactoryOptions<ApplicationPermissionManager> options, IOwinContext context)
        {
            return ApplicationPermissionManager.Create(context.Get<ApplicationDbContext>());
        }

        public static ApplicationPermissionManager Create(ApplicationDbContext context)
        {
            return new ApplicationPermissionManager(context);
        }
    }

    public partial class ApplicationSignInManager
    {
        // Methods
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return ApplicationSignInManager.Create(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public static ApplicationSignInManager Create(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        {
            return new ApplicationSignInManager(userManager, authenticationManager);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync(this.UserManager as ApplicationUserManager);
        }
    }


    // Identity
    public partial class ApplicationUser
    {
        // Methods
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }
    }

    public partial class ApplicationRole
    {
        // Methods

    }

    public partial class ApplicationPermission
    {
        // Methods

    }


    // Service
    public class EmailService : IIdentityMessageService
    {
        // Methods
        public Task SendAsync(IdentityMessage message)
        {
            // 將您的電子郵件服務外掛到這裡以傳送電子郵件。
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        // Methods
        public Task SendAsync(IdentityMessage message)
        {
            // 將您的 SMS 服務外掛到這裡以傳送簡訊。
            return Task.FromResult(0);
        }
    }
}
