using OMS.Core.Models.App;
using System;

namespace OMS.Core.Models.Permissions
{
    public interface IPermission
    {
        int PermissionID { get; set; }
        int ScreenID { get; set; }
        bool CanCreate { get; set; }
        bool CanRead { get; set; }
        bool CanUpdate { get; set; }
        bool CanDelete { get; set; }
        DateTime CreatedDate { get; set; }
        int CreatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        int UpdatedBy { get; set; }

        IScreen Screen { get; set; }
    }
}
