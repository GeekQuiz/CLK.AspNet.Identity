using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        // Constructors
        public IdentityDbContext() : this("DefaultConnection") { }

        public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }

        public IdentityDbContext(DbCompiledModel model) : base(model) { }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model) { }
    }

    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, IdentityPermission, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim, IdentityPermissionRole>
        where TUser : IdentityUser
    {
        // Constructors
        public IdentityDbContext() : this("DefaultConnection") { }

        public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }

        public IdentityDbContext(DbCompiledModel model) : base(model) { }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model) { }
    }

    public class IdentityDbContext<TUser, TRole, TPermission, TKey, TUserLogin, TUserRole, TUserClaim, TPermissionRole> : IdentityDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole, TPermissionRole>
        where TPermission : IdentityPermission<TKey, TPermissionRole>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TPermissionRole : IdentityPermissionRole<TKey>
    {
        // Constructors
        public IdentityDbContext() : this("DefaultConnection") { }

        public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }

        public IdentityDbContext(DbCompiledModel model) : base(model) { }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model) { }


        // Properties
        public virtual IDbSet<TPermission> Permissions { get; set; }
        

        // Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Contracts

            if (modelBuilder == null) throw new ArgumentNullException("modelBuilder");

            #endregion

            // Base
            base.OnModelCreating(modelBuilder);

            // Role
            var role = modelBuilder.Entity<TRole>()
                .ToTable("AspNetRoles");
            
            role.HasMany(r => r.Permissions).WithRequired().HasForeignKey(pr => pr.PermissionId);
        
            // Permission
            var permission = modelBuilder.Entity<TPermission>()
                .ToTable("AspNetPermissions");

            permission.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("PermissionNameIndex") { IsUnique = true }));

            permission.HasMany(p => p.Roles).WithRequired().HasForeignKey(pr => pr.PermissionId);

            // PermissionRole
            var permissionRole = modelBuilder.Entity<TPermissionRole>()
                .HasKey(pr => new { pr.PermissionId, pr.RoleId })
                .ToTable("AspNetPermissionRoles");
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            // Validate
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                // Result
                var errors = new List<DbValidationError>();

                // Permission
                var permission = entityEntry.Entity as TPermission;
                if (permission != null)
                {
                    if (this.Permissions.Any(p => String.Equals(p.Name, permission.Name)))
                    {
                        errors.Add(new DbValidationError("Permission", String.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicatePermissionName, permission.Name)));
                    }
                }

                // Return
                if (errors.Any() == true)
                {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }

            // Base
            return base.ValidateEntity(entityEntry, items);
        }
    }
}