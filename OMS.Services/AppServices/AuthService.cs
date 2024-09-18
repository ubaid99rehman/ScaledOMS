using DevExpress.Xpf.Core.Native;
using OMS.Core.Core.Models.User;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.DataAccess.Repositories.AppRepositories;
using OMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Services.AppServices
{
    public class AuthService : IAuthService
    {

        public AuthService(IUserRepository userRepository)
        {
            _userRespository = userRepository;
        }


        private IUserRepository _userRespository;

        public User UserAuth(string username, string password, out string message, out int isDisabled)
        {

            bool isAuthenticated = _userRespository.AuthenticateUser(username, password, out message, out isDisabled, out int userID);
            if(isAuthenticated)
            {
                var user = _userRespository.GetById(userID);
                if (user !=null)
                {
                    return user;
                }
            }
            return null;
        }

        public User Authenticate(UserCredentials credentials)
        {
            return  UserAuth(credentials.Username,credentials.Password, out string MessageBoxHelper, out int userID);
        }
    }
}
