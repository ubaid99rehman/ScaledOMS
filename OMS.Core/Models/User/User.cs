using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Core.Models.User
{
    public class User
    {
        public int UserID;
        public string UserName;
        public string Email;
        private string _password;
        bool UserStatus;

        public User() { }

        public User(int userID, string userName, string email, bool userStatus)
        {
            UserID = userID;
            UserName = userName;
            Email = email;
            UserStatus = userStatus;
        }
        public string Password
        {
            get => _password;
            set => _password = value;
        }

    }
}
