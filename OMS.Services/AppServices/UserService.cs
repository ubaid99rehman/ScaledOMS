using OMS.Core.Core.Models.User;
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

        public UserService(ICacheService cacheService, IUserRepository userRepository) 
        {
            CacheService = cacheService;
            UserRepository = userRepository;
        }

        public User GetUser()
        {
            if(CacheService.ContainsKey("CurrentUser"))
            {
                return CacheService.Get<User>("CurrentUser");
            }
            return null;
        }

        public bool UpdateUser(User user)
        {
            return UserRepository.UpdateUser(user);
        }
    }
}
