using OMS.Core.Core.Models.User;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;

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

        //Public Access Method Implementation
        public IUser Authenticate(UserCredentials credentials)
        {
            var user = UserAuth(credentials.Username, credentials.Password, out string MessageBoxHelper, out int userID);
            if (user is User && user != null)
            {
                CacheService.Set("CurrentUser", user);
            }
            return user;
        }
        
        //Prvate Method 
        public IUser UserAuth(string username, string password, out string message, out int isDisabled)
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
    }
}
