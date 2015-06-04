using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CLK.AspNet.Identity
{
    public class PermissionManager<TPermission, TKey> : IDisposable
        where TPermission : class, IPermission<TKey>
        where TKey : IEquatable<TKey>
    {
        // Fields
        private bool _disposed = false;

        private IPermissionStore<TPermission, TKey> _store = null;

        private PermissionValidator<TPermission, TKey> _validator = null;


        // Constructors
        public PermissionManager(IPermissionStore<TPermission, TKey> store)
        {
            #region Contracts

            if (store == null) throw new ArgumentNullException("store");

            #endregion

            // Default
            _store = store;
            _validator = new PermissionValidator<TPermission, TKey>(store);
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
            if (disposing == true && _disposed == false)
            {
                this.Store.Dispose();
                _disposed = true;
            }
        }


        // Properties
        protected internal IPermissionStore<TPermission, TKey> Store
        {
            get
            {
                // Require
                this.ThrowIfDisposed();

                // Return
                return _store;                
            }
        }

        public IIdentityValidator<TPermission> PermissionValidator
        {
            get
            {
                // Require
                this.ThrowIfDisposed();

                // Return
                return _validator;
            }
        }

        public virtual IQueryable<TPermission> Permissions
        {
            get
            {
                // Require
                this.ThrowIfDisposed();

                // QueryableStore
                var queryableStore = this.Store as IQueryablePermissionStore<TPermission, TKey>;
                if (queryableStore == null) throw new NotSupportedException(Resources.StoreNotIQueryablePermissionStore);

                // Return
                return queryableStore.Permissions;
            }
        }


        // Methods
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }


        public virtual async Task<IdentityResult> CreateAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Validate
            var result = await this.PermissionValidator.ValidateAsync(permission).WithCurrentCulture();
            if (result.Succeeded == false) return result;

            // Create
            await this.Store.CreateAsync(permission).WithCurrentCulture();

            // Return
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> UpdateAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Validate
            var result = await this.PermissionValidator.ValidateAsync(permission).WithCurrentCulture();
            if (result.Succeeded == false) return result;

            // Update
            await this.Store.UpdateAsync(permission).WithCurrentCulture();

            // Return
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> DeleteAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // Validate
            var result = await this.PermissionValidator.ValidateAsync(permission).WithCurrentCulture();
            if (result.Succeeded == false) return result;

            // Delete
            await this.Store.DeleteAsync(permission).WithCurrentCulture();

            // Return
            return IdentityResult.Success;
        }

        public virtual Task<TPermission> FindByIdAsync(TKey permissionId)
        {
            // Require
            this.ThrowIfDisposed();

            // FindById
            return this.Store.FindByIdAsync(permissionId);
        }

        public virtual Task<TPermission> FindByNameAsync(string permissionName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(permissionName) == true) throw new ArgumentNullException("permissionName");

            #endregion

            // Require
            this.ThrowIfDisposed();
            
            // FindByName
            return this.Store.FindByNameAsync(permissionName);
        }

        
        public virtual async Task<IdentityResult> AddToRoleAsync(TKey permissionId, string roleName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(roleName) == true) throw new ArgumentNullException("roleName");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // PermissionRoleStore
            var permissionRoleStore = this.Store as IPermissionRoleStore<TPermission, TKey>;
            if (permissionRoleStore == null) throw new NotSupportedException(Resources.StoreNotIPermissionRoleStore);

            // Permission
            var permission = await this.FindByIdAsync(permissionId).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // PermissionRoles
            var permissionRoles = await permissionRoleStore.GetRolesAsync(permission).WithCurrentCulture();
            if (permissionRoles == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // Add
            if (permissionRoles.Contains(roleName) == true)
            {
                return new IdentityResult(Resources.PermissionAlreadyInRole);
            }
            await permissionRoleStore.AddToRoleAsync(permission, roleName).WithCurrentCulture();

            // Update
            return await UpdateAsync(permission).WithCurrentCulture();
        }

        public virtual async Task<IdentityResult> AddToRolesAsync(TKey permissionId, params string[] roleNames)
        {
            #region Contracts

            if (roleNames == null) throw new ArgumentNullException("roleNames");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // PermissionRoleStore
            var permissionRoleStore = this.Store as IPermissionRoleStore<TPermission, TKey>;
            if (permissionRoleStore == null) throw new NotSupportedException(Resources.StoreNotIPermissionRoleStore);

            // Permission
            var permission = await this.FindByIdAsync(permissionId).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // PermissionRoles
            var permissionRoles = await permissionRoleStore.GetRolesAsync(permission).WithCurrentCulture();
            if (permissionRoles == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // Add
            foreach (var roleName in roleNames)
            {
                if (permissionRoles.Contains(roleName) == true)
                {
                    return new IdentityResult(Resources.PermissionAlreadyInRole);
                }
                await permissionRoleStore.AddToRoleAsync(permission, roleName).WithCurrentCulture();
            }

            // Update
            return await UpdateAsync(permission).WithCurrentCulture();
        }

        public virtual async Task<IdentityResult> RemoveFromRoleAsync(TKey permissionId, string roleName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(roleName) == true) throw new ArgumentNullException("roleName");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // PermissionRoleStore
            var permissionRoleStore = this.Store as IPermissionRoleStore<TPermission, TKey>;
            if (permissionRoleStore == null) throw new NotSupportedException(Resources.StoreNotIPermissionRoleStore);

            // Permission
            var permission = await this.FindByIdAsync(permissionId).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // PermissionRoles
            var permissionRoles = await permissionRoleStore.GetRolesAsync(permission).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));
            
            // Remove
            if (permissionRoles.Contains(roleName) == false)
            {
                return new IdentityResult(Resources.PermissionNotInRole);
            }
            await permissionRoleStore.RemoveFromRoleAsync(permission, roleName).WithCurrentCulture();

            // Update
            return await UpdateAsync(permission).WithCurrentCulture();
        }

        public virtual async Task<IdentityResult> RemoveFromRolesAsync(TKey permissionId, params string[] roleNames)
        {
            #region Contracts

            if (roleNames == null) throw new ArgumentNullException("roleNames");

            #endregion

            // Require
            this.ThrowIfDisposed();

            // PermissionRoleStore
            var permissionRoleStore = this.Store as IPermissionRoleStore<TPermission, TKey>;
            if (permissionRoleStore == null) throw new NotSupportedException(Resources.StoreNotIPermissionRoleStore);

            // Permission
            var permission = await this.FindByIdAsync(permissionId).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // PermissionRoles
            var permissionRoles = await permissionRoleStore.GetRolesAsync(permission).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // Remove
            foreach (var roleName in roleNames)
            {
                if (permissionRoles.Contains(roleName) == false)
                {
                    return new IdentityResult(Resources.PermissionNotInRole);
                }
                await permissionRoleStore.RemoveFromRoleAsync(permission, roleName).WithCurrentCulture();
            }

            // Update
            return await UpdateAsync(permission).WithCurrentCulture();
        }

        public virtual async Task<IList<string>> GetRolesByIdAsync(TKey permissionId)
        {
            // Require
            this.ThrowIfDisposed();

            // PermissionRoleStore
            var permissionRoleStore = this.Store as IPermissionRoleStore<TPermission, TKey>;
            if (permissionRoleStore == null) throw new NotSupportedException(Resources.StoreNotIPermissionRoleStore);

            // Permission
            var permission = await this.FindByIdAsync(permissionId).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionIdNotFound, permissionId));

            // GetRoles
            return await permissionRoleStore.GetRolesAsync(permission).WithCurrentCulture();
        }

        public virtual async Task<IList<string>> GetRolesByNameAsync(string permissionName)
        {
            // Require
            this.ThrowIfDisposed();

            // PermissionRoleStore
            var permissionRoleStore = this.Store as IPermissionRoleStore<TPermission, TKey>;
            if (permissionRoleStore == null) throw new NotSupportedException(Resources.StoreNotIPermissionRoleStore);

            // Permission
            var permission = await this.FindByNameAsync(permissionName).WithCurrentCulture();
            if (permission == null) throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.PermissionNameNotFound, permissionName));

            // GetRoles
            return await permissionRoleStore.GetRolesAsync(permission).WithCurrentCulture();
        }
    }
}