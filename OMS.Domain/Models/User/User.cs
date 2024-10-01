using OMS.Core.Models;
using OMS.Core.Models.User;

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
        #endregion

        #region Constuctors
        public User() { }
        public User(int userID, string userName, string email, bool userStatus)
        {
            UserID = userID;
            UserName = userName;
            Email = email;
            UserStatus = userStatus;
        } 
        #endregion

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
    }
}
