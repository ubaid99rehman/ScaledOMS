using DevExpress.Mvvm.Native;
using OMS.Core.Models.Account;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.Services.AppServices
{ 
    public class AccountService : IAccountService
    {
        #region Services
        private IAccountRepository accountRepository;
        private ICacheService CacheService;
        #endregion

        #region Constructor
        public AccountService(IAccountRepository _accountRepository, ICacheService cacheService)
        {
            accountRepository = _accountRepository;
            CacheService = cacheService;
        } 
        #endregion

        public ObservableCollection<IAccount> GetAll()
        {
            if (CacheService.ContainsKey("Accounts"))
            {
                return CacheService.Get<ObservableCollection<IAccount>>("Accounts");
            }
            bool accountsLoaded = LoadAccounts();
            if(accountsLoaded)
            {
                return CacheService.Get<ObservableCollection<IAccount>>("Accounts");
            }
            return new ObservableCollection<IAccount>();
        }
        public ObservableCollection<int> GetAccountsList()
        {
            if(CacheService.ContainsKey("AccountsList"))
            {
                return CacheService.Get<ObservableCollection<int>>("AccountsList");
            }
            return new ObservableCollection<int>();
        }
        public IAccount GetById(int key)
        {
            return GetAll().Where(a => a.AccountID == key).FirstOrDefault();
        }
        private bool LoadAccounts()
        {
            ObservableCollection<IAccount> accounts = accountRepository.GetAll().
                ToObservableCollection<IAccount>();
            ObservableCollection<int> accountsList = accounts.Select(s => s.AccountID).
                ToObservableCollection<int>();
            
            if (accounts != null && accounts.Count > 0)
            {
                CacheService.Set("Accounts", accounts);
                CacheService.Set("AccountsList", accountsList);
                return true;
            }
            return false;
        }
    }
}
