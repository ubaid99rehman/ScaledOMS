using DevExpress.Mvvm.Native;
using OMS.Core.Models.App;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.Services.AppServices
{
    public class PermissionService : IPermissionService
    {
        //Services
        private IUserService _userService;
        
        //Private Members
        private ObservableCollection<IUserRole> _UserRoles { get; set; }
        private ObservableCollection<IPermission> _UserPermissions { get; set; }

        //Public Members
        public ObservableCollection<IUserRole> UserRoles
        { get => _UserRoles; set => _UserRoles = value; }
        public ObservableCollection<IPermission> UserPermissions
        { get => _UserPermissions; set => _UserPermissions = value; }

        //Constructor
        public PermissionService()
        {
            UserRoles = new ObservableCollection<IUserRole>();
            UserPermissions = new ObservableCollection<IPermission>();
        }

        //Methods
        public void SetRolesAndPermissions(IUser user)
        {
            UserRoles = user.UserRoles.ToObservableCollection<IUserRole>();
            if (UserRoles != null && UserRoles.Count >= 1)
            {
                foreach (IUserRole userRole in user.UserRoles)
                {
                    ObservableCollection<IPermission> rolePermissions = userRole.Roles.Permissions.ToObservableCollection<IPermission>();
                    rolePermissions.ForEach(p => UserPermissions.Add(p));
                }
            }
        }
        public ObservableCollection<IUserRole> GetUserRoles()
        {
            return UserRoles;
        }
        public ObservableCollection<IPermission> GetUserViewPermissions()
        {
            return UserPermissions;
        }
        public bool HaveUserCancelPermission(string screen)
        {
            IPermission permission = UserPermissions.Where(p => p.Screen.ScreenName == screen).FirstOrDefault();
            if (permission == null)
            {
                return false;
            }
            return permission.CanDelete;
        }
        public bool HaveUserCreatePermission(string screen)
        {
            IPermission permission = UserPermissions.Where(p => p.Screen.ScreenName == screen).FirstOrDefault();
            if (permission == null)
            {
                return false;
            }
            return permission.CanCreate;
        }
        public bool HaveUserUpdatePermission(string screen)
        {
            IPermission permission = UserPermissions.Where(p => p.Screen.ScreenName == screen).FirstOrDefault();
            if (permission == null)
            {
                return false;
            }
            return permission.CanUpdate;
        }
    }
}
