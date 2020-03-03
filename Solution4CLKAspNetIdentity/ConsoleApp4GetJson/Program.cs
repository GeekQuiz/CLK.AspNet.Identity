using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4GetJson
{
    class Program
    {
        static void Main(string[] args)
        {
            //#region 使用JObject匿名物件(Sample) LOOK-->https://dotblogs.com.tw/shadow/2012/08/16/74099
            //string collapse_key = "source_update";
            //int time_to_live = 108;
            //bool delay_while_idle = true;
            //string score = "4x8";
            //string time = "15:16.2342";
            //List<string> registration_ids = new List<string>() { "4", "8", "15", "16", "23", "42" };

            ////JObject匿名物件
            //JObject obj = new JObject(
            //    new JProperty("collapse_key", collapse_key),
            //    new JProperty("time_to_live", time_to_live),
            //    new JProperty("delay_while_idle", delay_while_idle),
            //    new JProperty("data",
            //    new JObject(
            //        new JProperty("score", score),
            //        new JProperty("time", time))),
            //    new JProperty("registration_ids", registration_ids)
            //    );

            ////序列化為JSON字串並輸出結果
            //Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
            //Console.ReadLine();

            ///*Output:
            //             {
            //              "collapse_key": "source_update",
            //              "time_to_live": 108,
            //              "delay_while_idle": true,
            //              "data": {
            //              "score": "4x8",
            //              "time": "15:16.2342"
            //              },
            //              "registration_ids": [ "4", "8", "15", "16", "23", "42" ]
            //             }
            //*/
            //#endregion

            #region 預設的角色(Default - Role)
            List<string> Roles = new List<string>() { "Admin", "Guest" };
            #endregion
            #region 預設的讀取權限(Default - Permission)
            List<string> Permissions = new List<string>() { "AccessAccess", "ContactAccess", "ProductAddAccess", "ProductRemoveAccess" };
            #endregion
            #region 預設的使用者(Default - User)
            List<string> UserName = new List<string>() { "admin@example.com", "guest@example.com" };
            List<string> UserPassword = new List<string>() { "admin123", "guest123" };
            #endregion

            //JObject匿名物件
            JObject IdObj = new JObject(
                new JProperty("Roles", Roles),
                new JProperty("Permissions", Permissions),
                    new JProperty("Users", 
                    new JObject(
                        new JProperty("UserName", UserName[0]),
                        new JProperty("UserPassword", UserPassword[0])),
                    new JObject(
                        new JProperty("UserName", UserName[1]),
                        new JProperty("UserPassword", UserPassword[1]))
                    ));
            //序列化為JSON字串並輸出結果
            string Result = JsonConvert.SerializeObject(IdObj, Formatting.Indented);
            Console.WriteLine(Result);
            WriteJsonFile(Result, @"C:\Users\洪有德\source\repos\CLK.AspNet.Identity\Solution4CLKAspNetIdentity\WebApp4CLKAspNetIdentityUsingJson\IdObj.json");
            Console.ReadLine();
            /*Output
{
  "Roles": [
    "Admin",
    "Guest"
  ],
  "Permissions": [
    "AccessAccess",
    "ContactAccess",
    "ProductAddAccess",
    "ProductRemoveAccess"
  ],
  "Users": [
    {
      "UserName": "admin@example.com",
      "UserPassword": "admin123"
    },
    {
      "UserName": "guest@example.com",
      "UserPassword": "guest123"
    }
  ]
}
*/
        }

        private static void WriteJsonFile(string JsonString, string FileName)
        {
            FileStream fileStream = new FileStream(@FileName, FileMode.Create);
            fileStream.Close();   //切記開了要關,不然會被佔用而無法修改喔!!!
            using (StreamWriter sw = new StreamWriter(@FileName))
            {
                // 欲寫入的文字資料 ~
                sw.Write(JsonString);
            }
        }
    }
}
