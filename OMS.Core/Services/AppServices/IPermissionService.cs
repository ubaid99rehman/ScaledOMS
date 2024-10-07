using OMS.Core.Models.App;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using OMS.Core.Models.User;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.AppServices
{
    public interface IPermissionService
    {
        //Props
        ObservableCollection<IUserRole> UserRoles { get; set; }
        ObservableCollection<IPermission> UserPermissions { get; set; } 

        //Methods
        void SetRolesAndPermissions(IUser user);
        ObservableCollection<IUserRole> GetUserRoles();
        ObservableCollection<IPermission> GetUserViewPermissions();
        bool HaveUserUpdatePermission(string screen);
        bool HaveUserCreatePermission(string screen);
        bool HaveUserCancelPermission(string screen);
    }
}
