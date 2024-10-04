using OMS.Core.Models.Permissions;
using System;
using System.Collections.Generic;


namespace OMS.Core.Models.Roles
{
    public interface IRole
    {
        int RoleID { get; set; }
        string RoleName { get; set; }
        string RoleDescription { get; set; }
        DateTime CreatedDate { get; set; }
        int CreatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        int UpdatedBy { get; set; }
        ICollection<IPermission> Permissions { get; set; }
    }
}
