using DevExpress.Mvvm.Native;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.Services.AppServices
{
    public class UserService : IUserService
    {
        #region Services
        ICacheService CacheService;
        IPermissionService PermissionService;
        IUserRepository UserRepository;
        #endregion

        #region Constructor
        public UserService(ICacheService cacheService, IUserRepository userRepository,
            IPermissionService permissionService)
        {
            CacheService = cacheService;
            UserRepository = userRepository;
            PermissionService = permissionService;
        } 
        #endregion

        public bool LoadUsers()
        {
            ObservableCollection<IUser> Users = UserRepository.GetAll().ToObservableCollection<IUser>();
            if (Users != null && Users.Count > 0)
            {
                CacheService.Set("Users", Users);
                return true;
            }
            return false;
        }
        public ObservableCollection<IUser> GetAll()
        {
            if (CacheService.ContainsKey("Users"))
            {
                return CacheService.Get<ObservableCollection<IUser>>("Users");
            }
            bool usersLoaded = LoadUsers();
            if (usersLoaded)
            {
                return CacheService.Get<ObservableCollection<IUser>>("Users");
            }
            return new ObservableCollection<IUser>();
        }
        public IUser GetById(int key)
        {
            return GetAll().Where(u => u.UserID == key).FirstOrDefault();
        }

        public IUser SetUser(IUser user)
        {
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

        public bool Update(IUser user)
        {
            IUser updatedUser = UserRepository.UpdateUser(user);
            if (updatedUser != null && updatedUser.Email == user.Email)
            {
                return true;
            }
            return false;
        }
        public bool Add(IUser entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
