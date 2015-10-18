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

####[[ASP.NET MVC] 使用CLK.AspNet.Identity提供以角色為基礎的存取控制(RBAC)](http://www.dotblogs.com.tw/clark/archive/2015/06/08/151513.aspx)####

####[[ASP.NET MVC] 使用CLK.AspNet.Identity提供依權限顯示選單項目的功能](http://www.dotblogs.com.tw/clark/archive/2015/10/18/153597.aspx)####
