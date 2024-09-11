using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models.Account
{
    public class Account : BaseModel
    {
        private int _accountID;
        private string _accountName;
        private string _accountNumber;
        private DateTime _createdDate;
        
        public int AccountID
        {
            get => _accountID;
            set
            {
                if (_accountID != value)
                {
                    SetProperty(ref _accountID, value,nameof(AccountID));
                }
            }
        }

        public string AccountName
        {
            get => _accountName;
            set
            {
                if (_accountName != value)
                {
                    SetProperty(ref _accountName, value, nameof(AccountName));
                }
            }
        }

        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (_accountNumber != value)
                {
                    SetProperty(ref _accountNumber, value, nameof(AccountNumber));
                }
            }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set
            {
                if (_createdDate != value)
                {
                    SetProperty(ref _createdDate, value, nameof(CreatedDate));
                }
            }
        }

    }
}
