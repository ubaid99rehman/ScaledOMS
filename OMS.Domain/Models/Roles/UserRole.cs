using OMS.Core.Models.User;
using System;

namespace OMS.Core.Models.Roles
{
    public class UserRole
    {
        int UserID { get; set; }
        int RoleID { get; set; }
        DateTime AssignedDate { get; set; }

        IRole Roles { get; set; }
        IUser Users { get; set; }
    }
}
