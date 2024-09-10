using DevExpress.Mvvm.Native;
using OMS.Core.Models;
using OMS.Core.Models.Account;
using OMS.Core.Services.AppServices;
using OMS.DataAccess.UnitOfWork;
using OMS.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Services.AppServices
{ 
    public class AccountService : IAccountService
    {
        private IUnitOfWork unitOfWork;

        public AccountService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        private ObservableCollection<Account> accounts;
        private ObservableCollection<int> AccountsList;

        public ObservableCollection<int> GetAccountsList()
        {
            if(AccountsList == null)
            {
                foreach (var account in accounts)
                {
                    AccountsList.Add((int)account.AccountID);
                }
            }
            return AccountsList;
        }

        public ObservableCollection<Account> GetAll()
        {
            if(accounts == null)
            {
                accounts = unitOfWork.Accounts.GetAll().ToObservableCollection<Account>();
                return accounts;
            }
            return accounts;
        }

        public Account GetById(int key)
        {
            return unitOfWork.Accounts.GetById(key);
        }
    }
}
