﻿using OMS.Core.Models.User;
using System;

namespace OMS.Core.Models.Roles
{
    public interface IUserRole
    {
        int UserID { get; set; }
        int RoleID { get; set; }
        DateTime AssignedDate { get; set; }

        IRole Roles { get; set; }
        IUser Users { get; set; }
    }
}
