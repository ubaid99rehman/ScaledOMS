using OMS.Core.Models.User;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.AppServices
{
    public interface IUserService
    {
        IUser UpdateUser(IUser user);
        IUser GetUser();
        IUser SetUser(IUser user);
        ObservableCollection<IUser> GetUsers();
    }
}
