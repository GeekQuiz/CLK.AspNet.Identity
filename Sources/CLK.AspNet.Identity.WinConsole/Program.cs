using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.WinConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = CreateDb<CLK.AspNet.Identity.EntityFramework.IdentityDbContext>())
            {

            }

            Console.Write("End...");
            //Console.ReadLine();
        }

        static TContext CreateDb<TContext>()
            where TContext : global::System.Data.Entity.DbContext, new()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TContext>());
            var db = new TContext();
            db.Database.Initialize(true);
            return db;
        }
    }
}
