using CLK.AspNet.Identity.WebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CLK.AspNet.Identity.WebSite.Controllers
{
    [RBACAuthorize(Roles = "Admin")]
    public class PermissionsAdminController : Controller
    {
        // Constructors
        public PermissionsAdminController()
        {
        }

        public PermissionsAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ApplicationPermissionManager permissionManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.PermissionManager = permissionManager;
        }


        // Properties
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private ApplicationPermissionManager _permissionManager;
        public ApplicationPermissionManager PermissionManager
        {
            get
            {
                return _permissionManager ?? HttpContext.GetOwinContext().Get<ApplicationPermissionManager>();
            }
            private set
            {
                _permissionManager = value;
            }
        }
    }
}
