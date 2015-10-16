using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLK.AspNet.Identity.WebSite.Controllers
{    
    public class ProductController : Controller
    {
        [RBACAuthorize(Permission = "ProductAddAccess")]
        public ActionResult Add()
        {
            ViewBag.Message = "Your product add page.";

            return View();
        }

        [RBACAuthorize(Permission = "ProductRemoveAccess")]
        public ActionResult Remove()
        {
            ViewBag.Message = "Your product remove page.";

            return View();
        }
    }
}