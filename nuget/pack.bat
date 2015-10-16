
rem Copy
xcopy /S/Y/I ..\src\CLK.AspNet.Identity.WebSite CLK.AspNet.Identity.Mvc.Template

rem Rename
forfiles /S /M *.cs     /C "cmd /c rename @file @fname.cs.pp"
forfiles /S /M *.cshtml /C "cmd /c rename @file @fname.cshtml.pp"
forfiles /S /M *.asax   /C "cmd /c rename @file @fname.asax.pp"
forfiles /S /M *.config /C "cmd /c rename @file @fname.config.transform"

rem Pack
NuGet.exe pack CLK.AspNet.Identity.nuspec -Version 2.2.2
NuGet.exe pack CLK.AspNet.Identity.EntityFramework.nuspec -Version 2.2.2
NuGet.exe pack CLK.AspNet.Identity.Mvc.nuspec -Version 2.2.2
NuGet.exe pack CLK.AspNet.Identity.Mvc.Template.nuspec -Version 2.2.2

rem Delete
RD /S /Q CLK.AspNet.Identity.Mvc.Template