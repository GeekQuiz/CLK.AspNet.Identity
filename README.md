#[ASP.NET MVC] 使用CLK.AspNet.Identity提供以角色為基礎的存取控制(RBAC)#


##前言##

ASP.NET Identity是微軟所貢獻的開源專案，用來提供ASP.NET的驗證、授權機制。而在ASP.NET Identity的功能模組中：是採用Claims-Based驗證來提供驗證機制、並且實作Role-Based授權來提供授權機制。開發人員在系統內套用ASP.NET Identity後，就可以像下列範例一樣定義使用者屬於哪個角色、哪個角色可以使用那些功能，後續使用者通過驗證之後，就可以依照角色授權來使用系統功能。

ASP.NET Identity授權機制，可以在系統運行中動態變更使用者所屬的角色，但是卻不能動態變更角色可以使用的功能。這是因為在ASP.NET Identity裡，使用者屬於哪個角色的設定儲存於資料庫可以動態變更，而角色可以使用那些功能的設定則是定義在程式碼沒有辦法動態變更。雖然這樣的授權機制已經可以符合大部分的開發需求，但在需要動態變更角色使用那些功能的開發專案中，開發人員就沒有機會使用到ASP.NET Identity豐富的驗證授權機制。

- 領域模型

	![前言01](http://Files.Dotblogs.com.tw/clark/1506/201568171745823.png)

- 角色可以使用那些功能

	    public class HomeController : Controller
	    {
			[Authorize(Roles = "Admin")]
	        public ActionResult Contact() { ... }
	
	        [Authorize(Roles = "Guest")]
	        public ActionResult Contact() { ... }
	    }

- 使用者屬於哪個角色

	![前言02](http://Files.Dotblogs.com.tw/clark/1506/2015610103715901.png)


本篇文章介紹一個基於ASP.NET Identity開發設計的驗證授權模組：CLK.AspNet.Identity。這個驗證授權模組提供[以角色為基礎的存取控制(Role-based access control, RBAC)](http://en.wikipedia.org/wiki/Role-based_access_control)，將系統授權拆解為User(使用者)、Role(角色)、Permission(權限)。開發人員在系統內套用CLK.AspNet.Identity後，就可以像下列範例一樣定義使用者屬於哪個角色、哪個角色擁有那些權限、權限可以使用哪些功能，後續使用者通過驗證之後，就可以依照角色權限來使用系統功能。

CLK.AspNet.Identity授權機制，除了可以繼續使用繼承自ASP.NET Identity的Claims-Based驗證機制之外，也可以在系統運行中動態變更儲存於資料庫的授權設定：使用者所屬的角色、角色擁有的權限，讓系統的授權設定更加靈活多變，用以滿足更多的用戶需求。

- 領域模型

	![前言03](http://Files.Dotblogs.com.tw/clark/1506/201568171826144.png)
 
- 權限可以使用哪些功能

	    public class HomeController : Controller
	    {
	        [RBACAuthorize(Permission = "AboutAccess")]
	        public ActionResult Contact() { ... }
	
	        [RBACAuthorize(Permission = "ContactAccess")]
	        public ActionResult Contact() { ... }
	    }

- 權限屬於哪個角色

	![前言04](http://Files.Dotblogs.com.tw/clark/1506/2015610103748604.png)

- 使用者屬於哪個角色

	![前言05](http://Files.Dotblogs.com.tw/clark/1506/201561010389416.png)

##安裝##

1. 首先開啟Visual Studio建立一個「**完全空白**」的ASP.NET Web 應用程式。

	![安裝01](http://Files.Dotblogs.com.tw/clark/1506/201568173816454.png)

	![安裝02](http://Files.Dotblogs.com.tw/clark/1506/201568211216805.png)

2. 接著開啟NuGet管理工具，搜尋並安裝：「[**CLK.AspNet.Identity.Mvc Template**](https://www.nuget.org/packages/CLK.AspNet.Identity.Mvc.Template/)」

	![安裝03](http://Files.Dotblogs.com.tw/clark/1506/201568211412386.png)

3. 安裝需要花費一些時間，安裝完畢後即可看到必要檔案都已加入至專案。

	![安裝04](http://Files.Dotblogs.com.tw/clark/1506/201568211655197.png)

4. 安裝好CLK.AspNet.Identity之後，按下Visual Studio的執行按鈕，就可以在瀏覽器上看到預設的首頁內容。

	![安裝05](http://Files.Dotblogs.com.tw/clark/1506/201568211913632.png)


##變更角色的權限##

1. 使用預設的訪客帳號登入(ID:guest@example.com, PW:guest)，點擊頁面選單按鈕：About，因為guest@example.com屬於Guest群組、而Guest群組沒有AboutAccess權限，所以會收到403拒絕訪問的頁面內容。

	![變更角色的權限01](http://Files.Dotblogs.com.tw/clark/1506/201568212511810.png)

	![變更角色的權限02](http://Files.Dotblogs.com.tw/clark/1506/201568212623575.png)

	![變更角色的權限03](http://Files.Dotblogs.com.tw/clark/1506/2015610103959760.png)

2. 使用預設的管理帳號登入(ID:admin@example.com, PW:admin)，點擊頁面選單按鈕：PermissionsAdmin進入權限管理頁面，編輯AboutAccess權限，讓Guest群組擁有AboutAccess權限。

	![變更角色的權限04](http://Files.Dotblogs.com.tw/clark/1506/201568213346453.png)

	![變更角色的權限05](http://Files.Dotblogs.com.tw/clark/1506/201561010416901.png)

3. 更換回預設的訪客帳號登入(ID:guest@example.com, PW:guest)，點擊頁面選單按鈕：About，因為現在Guest群組擁有AboutAccess權限，所以可以瀏覽About頁面內容。

	![變更角色的權限06](http://Files.Dotblogs.com.tw/clark/1506/201568213543557.png)


##變更使用者的角色##

1. 使用預設的訪客帳號登入(ID:guest@example.com, PW:guest)，點擊頁面選單按鈕：Contact，因為guest@example.com屬於Guest群組、而Guest群組沒有ContactAccess權限，所以會收到403拒絕訪問的頁面內容。

	![變更使用者的角色01](http://Files.Dotblogs.com.tw/clark/1506/20156821450160.png)

	![變更使用者的角色02](http://Files.Dotblogs.com.tw/clark/1506/201568214514379.png)

	![變更使用者的角色03](http://Files.Dotblogs.com.tw/clark/1506/2015610104129448.png)

2. 使用預設的管理帳號登入(ID:admin@example.com, PW:admin)，點擊頁面選單按鈕：UsersAdmin進入使用者管理頁面，編輯guest@example.com使用者，讓guest@example.com使用者加入到Admin群組。

	![變更使用者的角色04](http://Files.Dotblogs.com.tw/clark/1506/201568214722691.png)

	![變更使用者的角色05](http://Files.Dotblogs.com.tw/clark/1506/2015610104146120.png)

3. 更換回預設的訪客帳號登入(ID:guest@example.com, PW:guest)，點擊頁面選單按鈕：Contact，因為現在guest@example.com屬於Admin群組，而Admin群組擁有ContactAccess權限，所以可以瀏覽Contact頁面內容。

	![變更使用者的角色06](http://Files.Dotblogs.com.tw/clark/1506/201568214835196.png)


##新增系統的權限##

1. 回到Visual Studio編輯新功能，首先在HomeController增加一個新功能「News」、設定NewsAccess權限可以使用這個功能，並且在Viwes裡面加上對應的變更。

	    public class HomeController : Controller
	    {
	        [RBACAuthorize(Permission = "NewsAccess")]
	        public ActionResult News()
	        {
	            ViewBag.Message = "Your news page.";
	
	            return View();
	        }
	    }

2. 按下Visual Studio的執行按鈕，可以在瀏覽器上看到預設的首頁內容，並且內容中多了一個名稱為News的頁面選單按鈕。

	![新增系統的權限01](http://Files.Dotblogs.com.tw/clark/1506/201568215748334.png)

3. 使用預設的訪客帳號登入(ID:guest@example.com, PW:guest)，點擊頁面選單按鈕：News，這時因為系統裡沒有設定NewsAccess權限，所以會收到PermissionName not found.的錯誤訊息頁面。

	![新增系統的權限02](http://Files.Dotblogs.com.tw/clark/1506/2015682260855.png)

	![新增系統的權限03](http://Files.Dotblogs.com.tw/clark/1506/20156822632922.png)

	![新增系統的權限04](http://Files.Dotblogs.com.tw/clark/1506/2015610103828666.png)

4. 使用預設的管理帳號登入(ID:admin@example.com, PW:admin)，點擊頁面選單按鈕：PermissionsAdmin進入權限管理頁面，新增NewsAccess權限，並且讓Guest群組擁有NewsAccess權限。

	![新增系統的權限05](http://Files.Dotblogs.com.tw/clark/1506/20156822124954.png)
	
	![新增系統的權限06](http://Files.Dotblogs.com.tw/clark/1506/2015610103922932.png)

5. 更換回預設的訪客帳號登入(ID:guest@example.com, PW:guest)，點擊頁面選單按鈕：News，因為現在Guest群組擁有NewsAccess權限，所以可以瀏覽News頁面內容。

	![新增系統的權限07](http://Files.Dotblogs.com.tw/clark/1506/20156822178726.png)

