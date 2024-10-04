using OMS.Core.Models.User;
using System;

namespace OMS.Core.Models.Permissions
{
    public interface IUserPermission
    {
        int UserID { get; set; }
        int PermissionID { get; set; }
        DateTime AssignedDate { get; set; }

        IPermission Permissions { get; set; }
        IUser Users { get; set; }
    }
}
