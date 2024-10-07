using DevExpress.Mvvm.Native;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using System.Collections.ObjectModel;

namespace OMS.Services.AppServices
{
    public class AuthService : IAuthService
    {
        ICacheService CacheService;
        private IUserRepository _userRespository;

        //Constructor
        public AuthService(IUserRepository userRepository, ICacheService cache)
        {
            _userRespository = userRepository;
            CacheService = cache;
        }

        //Public Access Method
        public IUser Authenticate(UserCredentials credentials)
        {
            IUser user = UserAuth(credentials);
            if (user is IUser && user != null)
            {
                return user;
            }
            return null;
        }
        
        //Prvate Method 
        private IUser UserAuth(UserCredentials credentials)
        {
            IUser user = _userRespository.AuthenticateUser(credentials);
            if(user != null)
            {
                if (user !=null)
                {
                    if(user.UserStatus && user.PasswordRetryCount > 0)
                    {
                        return user;
                    }
                }
            }
            return null;
        }
    }
}
