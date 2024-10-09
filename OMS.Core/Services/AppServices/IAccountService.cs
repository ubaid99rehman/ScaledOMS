using OMS.Core.Models.Account;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.AppServices
{
    public interface IAccountService : IReadonlyService<IAccount>
    {
        ObservableCollection<int> GetAccountsList();
    }
}
