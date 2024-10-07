using DevExpress.Mvvm.Native;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using System.Collections.ObjectModel;

namespace OMS.Services.AppServices
{
    public class UserService : IUserService
    {
        //Services
        ICacheService CacheService;
        IPermissionService PermissionService;
        IUserRepository UserRepository;

        //Constructor
        public UserService(ICacheService cacheService, IUserRepository userRepository, IPermissionService permissionService) 
        {
            CacheService = cacheService;
            UserRepository = userRepository;
            PermissionService = permissionService;
        }
        
        //Public Data Access Methods Implementation
        public IUser SetUser(IUser user)
        {
            //Set User, Roles and Permissions
            CacheService.Set("CurrentUser", user);
            PermissionService.SetRolesAndPermissions(user);
            return GetUser();
        }
        public IUser GetUser()
        {
            if(CacheService.ContainsKey("CurrentUser"))
            {
                return CacheService.Get<IUser>("CurrentUser");
            }
            return null;
        }
        public IUser UpdateUser(IUser user)
        {
            return UserRepository.UpdateUser(user);
        }

        public ObservableCollection<IUser> GetUsers()
        {
            return UserRepository.GetAll().ToObservableCollection<IUser>();
        }
    }
}
