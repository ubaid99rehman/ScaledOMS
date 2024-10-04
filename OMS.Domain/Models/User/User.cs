using OMS.Core.Models;
using OMS.Core.Models.Account;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using OMS.Core.Models.User;
using System;
using System.Collections.Generic;

namespace OMS.Core.Core.Models.User
{
    public class User : BaseModel, IUser
    {
        #region Private Members
        private int _userID;
        private string _userName;
        private string _email;
        private bool _userStatus;
        private string _password;
        private bool _defaultPassword;
        private int _passwordRetryCount;
        private DateTime _lastPasswordChangeDate;
        private DateTime _createdDate;
        private DateTime _updatedDate;
        private int _createdBy;
        private int _updatedBy;
        private ICollection<IUserAccount> _userAccounts;
        private ICollection<IUserPermission> _userPermissions;
        private ICollection<IUserRole> _userRoles;
        #endregion

        #region Constuctors
        public User() 
        {
            UserID = -1;
        }
        public User(int userID, string userName, string email, bool userStatus)
        {
            UserID = userID;
            UserName = userName;
            Email = email;
            UserStatus = userStatus;
        } 
        #endregion

        //Public Members
        public int UserID
        {
            get => _userID; 
            set
            {
                SetProperty(ref _userID, value);
            }
        }
        public string UserName 
        { 
            get => _userName;
            set 
            {
                SetProperty(ref _userName, value);
            }
        }
        public string Email 
        { 
            get => _email;
            set
            {
                SetProperty(ref _email, value);
            }
        }
        public bool UserStatus 
        {
            get => _userStatus;  
            set
            {
                SetProperty(ref _userStatus, value);
            }
        }
        public string Password
        {
            get => _password;
            set => _password = value;
        }
        public DateTime CreatedDate
        {
            get => _createdDate;
            set
            {
                SetProperty(ref _createdDate, value);
            }
        }
        public DateTime UpdatedDate
        {
            get => _updatedDate;
            set
            {
                SetProperty(ref _updatedDate, value);
            }
        }
        public int CreatedBy
        {
            get => _createdBy;
            set
            {
                SetProperty(ref _createdBy, value);
            }

        }
        public int UpdatedBy
        {
            get => _updatedBy;
            set
            {
                SetProperty(ref _updatedBy, value);
            }
        }
        public DateTime LastPasswordChangeDate
        {
            get { return _lastPasswordChangeDate; }
            set
            {
                SetProperty(ref _lastPasswordChangeDate, value);
            }
        }
        public int PasswordRetryCount
        {
            get => _passwordRetryCount;
            set
            {
                SetProperty(ref _passwordRetryCount, value);
            }

        }
        public bool DefaultPassword
        {
            get => _defaultPassword;
            set
            {
                SetProperty(ref _defaultPassword, value);
            }
        }

        public ICollection<IUserAccount> UserAccounts
        {
            get => _userAccounts;
            set
            {
                SetProperty(ref _userAccounts, value);
            }

        }
        public ICollection<IUserPermission> UserPermissions
        {
            get => _userPermissions;
            set
            {
                SetProperty(ref _userPermissions, value);
            }
        }
        public ICollection<IUserRole> UserRoles
        {
            get => _userRoles;
            set
            { 
                SetProperty(ref _userRoles, value);
            }
        }
    }
}
