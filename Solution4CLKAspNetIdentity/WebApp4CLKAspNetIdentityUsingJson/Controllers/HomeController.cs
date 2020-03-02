using System.Web.Mvc;

namespace WebApp4CLKAspNetIdentityUsingJson.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [RBACAuthorize(Permission = "AccessAccess")]
        public ActionResult Access()
        {
            return View();
        }

        [RBACAuthorize(Permission = "ContactAccess")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}