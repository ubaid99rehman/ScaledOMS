using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using System;


namespace OMS.Services.AppServices
{
    public class UserService : IUserService
    {
        ICacheService CacheService;
        IUserRepository UserRepository;
        //Constructor
        public UserService(ICacheService cacheService, IUserRepository userRepository) 
        {
            CacheService = cacheService;
            UserRepository = userRepository;
        }
        
        //Public Data Access Methods Implementation
        public IUser GetUser()
        {
            if(CacheService.ContainsKey("CurrentUser"))
            {
                return CacheService.Get<IUser>("CurrentUser");
            }
            return null;
        }
        public bool UpdateUser(IUser user)
        {
            return UserRepository.UpdateUser(user);
        }
    }
}
