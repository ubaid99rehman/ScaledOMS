using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using OMS.Core.Models.User;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.AppServices
{
    public interface IPermissionService
    {
        void SetRolesAndPermissions(IUser user);
        ObservableCollection<IUserRole> GetUserRoles();
        ObservableCollection<IPermission> GetUserViewPermissions();
        //Check Permissions
        bool HaveUserUpdatePermission(string screen);
        bool HaveUserCreatePermission(string screen);
        bool HaveUserCancelPermission(string screen);
    }
}
