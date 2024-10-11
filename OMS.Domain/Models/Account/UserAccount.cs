using OMS.Core.Models;
using OMS.Core.Models.Account;
using OMS.Core.Models.User;
using System;

namespace OMS.Domain.Models.Account
{
    public class UserAccount : BaseModel, IUserAccount
    {
        private int _userID;
        private int _accountID;
        private DateTime _addedDate;
        private int _addedBy;
        private IUser _user;
        private IAccount _account;

        public int UserID
        {
            get => _userID;
            set => SetProperty(ref _userID, value); 
        }
        public int AccountID
        { 
            get => _accountID;
            set => SetProperty(ref _accountID, value);
        }
        public DateTime AddedDate
        {
            get => _addedDate;
            set => SetProperty(ref _addedDate, value);
        }
        public int AddedBy
        {
            get => _addedBy;
            set => SetProperty(ref _addedBy, value);
        }
        public IUser User
        {
            get => _user; 
            set => SetProperty(ref _user, value);
        }
        public IAccount Account
        {
            get => _account; 
            set => SetProperty(ref _account, value);
        }
    }
}
