using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace WebApp4CLKAspNetIdentityUsingJson
{
    //public class RootObject
    //{
    //    public List<User> Users { get; set; }
    //    public List<string> Roles { get; set; }
    //    public List<string> Permissions { get; set; }
    //}

    //public class User
    //{
    //    public string UserName { get; set; }
    //    public string UserPassword { get; set; }
    //}

    public class ReadJsonFile
    {

        public JObject IdObj(string JsonFile)
        { 
            return JObject.Parse(File.ReadAllText(@JsonFile));
        }

        public void Action(string JsonFile, out string[] UserName, out string[] UserPassword, out string[] Role, out string[] Permission)
        {
            ExportUsers(JsonFile, out UserName, out UserPassword);
            ExportRoles(JsonFile, out Role);
            ExportPermissions(JsonFile, out Permission);
        }

        public void ExportPermissions(string JsonFile, out string[] Permission)
        {
            JObject ThisIdObj = IdObj(JsonFile);

            IEnumerable<string> permission = from p in ThisIdObj["Permissions"] select (string)p;
            Permission = permission.ToArray();
        }

        public void ExportRoles(string JsonFile, out string[] Role)
        {
            JObject ThisIdObj = IdObj(JsonFile);

            IEnumerable<string> role = from r in ThisIdObj["Roles"] select (string)r;
            Role = role.ToArray();
        }

        public void ExportUsers(string JsonFile, out string[] UserName, out string[] UserPassword)
        {
            JObject ThisIdObj = IdObj(JsonFile);

            JArray users = (JArray)ThisIdObj["Users"];

            IEnumerable<string> userName = from uN in ThisIdObj["Users"] select (string)uN["UserName"];
            UserName = userName.ToArray();

            IEnumerable<string> userPassword = from uP in ThisIdObj["Users"] select (string)uP["UserPassword"];
            UserPassword = userPassword.ToArray();
        }
    }
}