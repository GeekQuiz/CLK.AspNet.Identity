#CLK.AspNet.Identity#

##簡介##

CLK.AspNet.Identity是一個基於ASP.NET Identity擴展設計的驗證授權模組，這個模組提供[以角色為基礎的存取控制(Role-based access control, RBAC)](http://en.wikipedia.org/wiki/Role-based_access_control)，將系統授權拆解為User(使用者)、Role(角色)、Permission(權限)。讓開發人員可以在系統內，定義使用者屬於哪個角色、哪個角色擁有那些權限、權限可以使用哪些功能。後續使用者通過驗證之後，就可以依照角色權限來使用系統功能。

    public class HomeController : Controller
    {
        [RBACAuthorize(Permission = "AboutAccess")]
        public ActionResult About()
        {
            return View();
        }

        [RBACAuthorize(Permission = "ContactAccess")]
        public ActionResult Contact()
        {
            return View();
        }
    }


##說明##

####[[ASP.NET MVC] 使用CLK.AspNet.Identity提供以角色為基礎的存取控制(RBAC)](http://clark159.github.io/2015/06/08/%5BASP.NET%20MVC%5D%20%E4%BD%BF%E7%94%A8CLK.AspNet.Identity%E6%8F%90%E4%BE%9B%E4%BB%A5%E8%A7%92%E8%89%B2%E7%82%BA%E5%9F%BA%E7%A4%8E%E7%9A%84%E5%AD%98%E5%8F%96%E6%8E%A7%E5%88%B6(RBAC)/)####
