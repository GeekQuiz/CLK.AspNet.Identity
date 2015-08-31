using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    // DropCreateForModelChanges
    public class DropCreateForModelChangesDbInitializer<TContext> : DropCreateDatabaseIfModelChanges<TContext>
        where TContext : System.Data.Entity.DbContext
    {
        // Constructors
        public DropCreateForModelChangesDbInitializer(Action<TContext> initializeIdentity = null)
        {
            // Default
            this.InitializeIdentity = initializeIdentity;
        }


        // Properties
        private Action<TContext> InitializeIdentity { get; set; }


        // Methods
        protected override void Seed(TContext context)
        {
            #region Contracts

            if (context == null) throw new ArgumentNullException("context");

            #endregion

            // Base
            base.Seed(context);

            // InitializeIdentity
            if (this.InitializeIdentity != null)
            {
                this.InitializeIdentity(context);
            }
        }
    }


    // DropCreateForAlways
    public class DropCreateForAlwaysDbInitializer<TContext> : DropCreateDatabaseAlways<TContext>
        where TContext : System.Data.Entity.DbContext
    {
        // Constructors
        public DropCreateForAlwaysDbInitializer(Action<TContext> initializeIdentity = null)
        {
            // Default
            this.InitializeIdentity = initializeIdentity;
        }


        // Properties
        private Action<TContext> InitializeIdentity { get; set; }


        // Methods
        protected override void Seed(TContext context)
        {
            #region Contracts

            if (context == null) throw new ArgumentNullException("context");

            #endregion

            // Base
            base.Seed(context);

            // InitializeIdentity
            if (this.InitializeIdentity != null)
            {
                this.InitializeIdentity(context);
            }
        }
    }
    

    // Migrate
    public class MigrateDbInitializer<TContext> : MigrateDatabaseToLatestVersion<TContext, MigrateDbInitializerConfiguration<TContext>>
        where TContext : System.Data.Entity.DbContext
    {
        // Constructors
        public MigrateDbInitializer(Action<TContext> initializeIdentity = null) : base(false, new MigrateDbInitializerConfiguration<TContext>(initializeIdentity)) { }
    }

    public class MigrateDbInitializerConfiguration<TContext> : DbMigrationsConfiguration<TContext>
        where TContext : System.Data.Entity.DbContext
    {
        // Constructors
        public MigrateDbInitializerConfiguration()
        {
            // Default
            this.InitializeIdentity = null;
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        public MigrateDbInitializerConfiguration(Action<TContext> initializeIdentity = null)
        {
            // Default
            this.InitializeIdentity = initializeIdentity;
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }


        // Properties
        private Action<TContext> InitializeIdentity { get; set; }


        // Methods
        protected override void Seed(TContext context)
        {
            #region Contracts

            if (context == null) throw new ArgumentNullException("context");

            #endregion

            // Base
            base.Seed(context);

            // InitializeIdentity
            if (this.InitializeIdentity != null)
            {
                this.InitializeIdentity(context);
            }
        }
    }
}
