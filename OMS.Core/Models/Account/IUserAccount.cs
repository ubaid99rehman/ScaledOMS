using OMS.Core.Models.User;
using System;

namespace OMS.Core.Models.Account
{
    public interface IUserAccount
    {
        int UserID { get; set; }
        int AccountID { get; set; }
        DateTime AddedDate { get; set; }
        int AddedBy { get; set; }

        IUser User { get; set; } 
        IAccount Account { get; set; }
    }
}
