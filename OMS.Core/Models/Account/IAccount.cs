using System;

namespace OMS.Core.Models.Account
{
    public interface IAccount
    {
        int AccountID { get; set; }
        string AccountName { get; set; }
        string AccountNumber { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
