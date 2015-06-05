using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity.EntityFramework
{
    public class PermissionStore<TRole, TPermission> : CLK.AspNet.Identity.IPermissionRoleStore<TPermission, string>, CLK.AspNet.Identity.IQueryablePermissionStore<TPermission, string>
        where TRole : CLK.AspNet.Identity.EntityFramework.IdentityRole, new()
        where TPermission : CLK.AspNet.Identity.EntityFramework.IdentityPermission, new()
    {
        // Fields
        private bool _disposed = false;

        private EntityStore<TRole> _roleStore = null;

        private EntityStore<TPermission> _permissionStore = null;

        private IDbSet<IdentityPermissionRole> _permissionRoles = null;


        // Constructors
        public PermissionStore(DbContext context)
        {
            #region Contracts

            if (context == null) throw new ArgumentNullException("context");

            #endregion

            // Default
            this.Context = context;
            this.AutoSaveChanges = true;
            _permissionStore = new EntityStore<TPermission>(context);
            _roleStore = new EntityStore<TRole>(context);
            _permissionRoles = context.Set<IdentityPermissionRole>();
        }

        public void Dispose()
        {
            // Dispose
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Dispose
            if (this.DisposeContext == true && disposing== true && Context != null)
            {
                this.Context.Dispose();
            }
            _disposed = true;
            this.Context = null;
            _permissionStore = null;
        }


        // Properties
        public DbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public bool AutoSaveChanges { get; set; }

        public IQueryable<TPermission> Permissions
        {
            get 
            { 
                return _permissionStore.EntitySet; 
            }
        }

        
        // Methods
        private void ThrowIfDisposed()
        {
            if (_disposed==true)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        private async Task SaveChanges()
        {
            if (this.AutoSaveChanges == true)
            {
                await this.Context.SaveChangesAsync().WithCurrentCulture();
            }
        }


        public virtual async Task CreateAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Create
            _permissionStore.Create(permission);

            // SaveChanges
            await this.SaveChanges().WithCurrentCulture();
        }
        
        public virtual async Task UpdateAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Update
            _permissionStore.Update(permission);

            // SaveChanges
            await this.SaveChanges().WithCurrentCulture();
        }

        public virtual async Task DeleteAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Delete
            _permissionStore.Delete(permission);

            // SaveChanges
            await this.SaveChanges().WithCurrentCulture();
        }

        public virtual async Task<TPermission> FindByIdAsync(string permissionId)
        {
            // Require
            this.ThrowIfDisposed();

            // Permission
            TPermission permission = await _permissionStore.GetByIdAsync(permissionId).WithCurrentCulture();
            if (permission == null) return null;

            // Ensure
            await EnsureRolesLoaded(permission).WithCurrentCulture();

            // Return
            return permission;
        }

        public virtual async Task<TPermission> FindByNameAsync(string permissionName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(permissionName) == true) throw new ArgumentNullException("permissionName");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Permission
            TPermission permission = await this.Permissions.FirstOrDefaultAsync(p => p.Name.ToUpper() == permissionName.ToUpper()).WithCurrentCulture();
            if (permission == null) return null;

            // Ensure
            await EnsureRolesLoaded(permission).WithCurrentCulture();

            // Return
            return permission;
        }
        
        private async Task EnsureRolesLoaded(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            if (this.Context.Entry(permission).Collection(p => p.Roles).IsLoaded == false)
            {
                await _permissionRoles.Where(pr => pr.PermissionId.Equals(permission.Id)).LoadAsync().WithCurrentCulture();
                this.Context.Entry(permission).Collection(p => p.Roles).IsLoaded = true;
            }
        }

       
        public virtual async Task AddToRoleAsync(TPermission permission, string roleName)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");
            if (string.IsNullOrEmpty(roleName) == true) throw new ArgumentNullException("roleName");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // RoleEntity
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (roleEntity == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, roleName));

            // Add
            _permissionRoles.Add(new IdentityPermissionRole { PermissionId = permission.Id, RoleId = roleEntity.Id });
        }

        public virtual async Task RemoveFromRoleAsync(TPermission permission, string roleName)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");
            if (string.IsNullOrEmpty(roleName) == true) throw new ArgumentNullException("roleName");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // RoleEntity
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (roleEntity == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, roleName));

            // PermissionRole
            var permissionRole = await _permissionRoles.FirstOrDefaultAsync(r => roleEntity.Id.Equals(r.RoleId) && r.PermissionId.Equals(permission.Id)).WithCurrentCulture();
            if (permissionRole == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, IdentityResources.PermissionRoleNotFound, roleName));

            // Remove
            _permissionRoles.Remove(permissionRole);
        }

        public virtual async Task<IList<string>> GetRolesAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Query
            var query = from permissionRole in _permissionRoles
                        where permissionRole.PermissionId.Equals(permission.Id)
                        join role in _roleStore.DbEntitySet on permissionRole.RoleId equals role.Id
                        select role.Name;
            return await query.ToListAsync().WithCurrentCulture();
        }
    }
}