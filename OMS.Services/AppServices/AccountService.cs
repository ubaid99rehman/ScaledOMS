using DevExpress.Mvvm.Native;
using OMS.Core.Models.Account;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.Services.AppServices
{ 
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;
        private ICacheService CacheService;

        //Constructor
        public AccountService(IAccountRepository _accountRepository, ICacheService cacheService)
        {
            accountRepository = _accountRepository;
            CacheService = cacheService;
        }

        //Public Methods Implementation
        public ObservableCollection<int> GetAccountsList()
        {
            if(CacheService.ContainsKey("AccountsList"))
            {
                return CacheService.Get<ObservableCollection<int>>("AccountsList");
            }
            return new ObservableCollection<int>( GetAll().Select(s => s.AccountID));
        }
        public ObservableCollection<IAccount> GetAll()
        {
            if (CacheService.ContainsKey("Accounts"))
            {
                return CacheService.Get<ObservableCollection<IAccount>>("Accounts");
            }
            return accountRepository.GetAll().ToObservableCollection<IAccount>();
        }
        public IAccount GetById(int key)
        {
            return GetAll().Where(a => a.AccountID == key).FirstOrDefault();
        }
    }
}
