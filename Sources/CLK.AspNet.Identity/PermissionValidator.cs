using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace CLK.AspNet.Identity
{
    public class PermissionValidator<TPermission> : PermissionValidator<TPermission, string>
        where TPermission : class, IPermission<string>
    {
        // Constructors
        public PermissionValidator(PermissionManager<TPermission, string> manager) : base(manager) { }
    }

    public class PermissionValidator<TPermission, TKey> : IIdentityValidator<TPermission>
        where TPermission : class, IPermission<TKey>
        where TKey : IEquatable<TKey>
    {
        // Constructors
        public PermissionValidator(PermissionManager<TPermission, TKey> manager)
        {
            #region Contracts

            if (manager == null) throw new ArgumentNullException("manager");

            #endregion

            // Default
            this.Manager = manager;
        }


        // Properties
        private PermissionManager<TPermission, TKey> Manager { get; set; }


        // Methods
        public virtual async Task<IdentityResult> ValidateAsync(TPermission permission)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");

            #endregion

            // Validate
            var errors = new List<string>();
            await ValidatePermissionName(permission, errors).WithCurrentCulture();            
            if (errors.Count > 0) return IdentityResult.Failed(errors.ToArray());

            // Return
            return IdentityResult.Success;
        }

        private async Task ValidatePermissionName(TPermission permission, List<string> errors)
        {
            #region Contracts

            if (permission == null) throw new ArgumentNullException("permission");
            if (errors == null) throw new ArgumentNullException("errors");

            #endregion

            if (string.IsNullOrWhiteSpace(permission.Name))
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Name"));
            }
            else
            {
                var owner = await this.Manager.FindByNameAsync(permission.Name).WithCurrentCulture();
                if (owner != null && EqualityComparer<TKey>.Default.Equals(owner.Id, permission.Id) == false)
                {
                    errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, permission.Name));
                }
            }
        }
    }
}