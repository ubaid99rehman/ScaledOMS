using OMS.Core.Models.Permissions;
using System;
using System.Collections.Generic;

namespace OMS.Core.Models.App
{
    public interface IScreen
    {
        int ScreenID { get; set; }
        string ScreenName { get; set; }
        string ScreenDescription { get; set; }
        DateTime CreatedDate { get; set; }
        int CreatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        int UpdatedBy { get; set; }
        ICollection<IPermission> Permissions { get; set; }
    }
}
