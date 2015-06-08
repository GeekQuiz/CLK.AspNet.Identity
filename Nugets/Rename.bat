forfiles /S /M *.cs     /C "cmd /c rename @file @fname.cs.pp"
forfiles /S /M *.cshtml /C "cmd /c rename @file @fname.cshtml.pp"
forfiles /S /M *.asax   /C "cmd /c rename @file @fname.asax.pp"
forfiles /S /M *.config /C "cmd /c rename @file @fname.config.transform"