using OMS.Core.Models.Account;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.AppServices
{
    public interface IAccountService : IMarketService<Account>
    {
        ObservableCollection<int> GetAccountsList();
    }
}
