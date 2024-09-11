using DevExpress.Mvvm.Native;
using OMS.Core.Models.Account;
using OMS.Core.Services;
using OMS.Core.Services.AppServices;
using OMS.DataAccess.Repositories;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.Services.AppServices
{ 
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;
        private ICacheService CacheService;

        public AccountService(IAccountRepository _accountRepository, ICacheService cacheService)
        {
            accountRepository = _accountRepository;
            CacheService = cacheService;
        }

        public ObservableCollection<int> GetAccountsList()
        {
            if(CacheService.ContainsKey("AccountsList"))
            {
                return CacheService.Get<ObservableCollection<int>>("AccountsList");
            }
            return new ObservableCollection<int>( GetAll().Select(s => s.AccountID));
        }

        public ObservableCollection<Account> GetAll()
        {
            if (CacheService.ContainsKey("Accounts"))
            {
                return CacheService.Get<ObservableCollection<Account>>("Accounts");
            }
            return accountRepository.GetAll().ToObservableCollection<Account>();
        }

        public Account GetById(int key)
        {
            return GetAll().Where(a => a.AccountID == key).FirstOrDefault();
        }
    }
}
