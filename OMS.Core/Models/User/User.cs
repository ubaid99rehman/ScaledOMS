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
        bool UserStatus;

        public User() { }

        public User(int userID, string userName, string email, bool userStatus)
        {
            UserID = userID;
            UserName = userName;
            Email = email;
            UserStatus = userStatus;
        }
    }
}
