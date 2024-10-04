
using OMS.Core.Models.Account;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using System;
using System.Collections.Generic;

namespace OMS.Core.Models.User
{
    public interface IUser
    {
        int UserID { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        int CreatedBy { get; set; }
        int UpdatedBy { get; set; }
        DateTime LastPasswordChangeDate { get; set; }
        int PasswordRetryCount { get; set; }
        bool DefaultPassword { get; set; }
        bool UserStatus { get; set; }

        ICollection<IUserAccount> UserAccounts { get; set; }
        ICollection<IUserPermission> UserPermissions { get; set; }
        ICollection<IUserRole> UserRoles { get; set; }
    }
}
