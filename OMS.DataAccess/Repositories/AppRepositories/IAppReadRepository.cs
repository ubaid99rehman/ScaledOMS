using OMS.Core.Core.Models.User;
using OMS.Core.Models.Account;
using System.Collections.Generic;


namespace OMS.DataAccess.Repositories.AppRepositories
{
    public interface IAppReadRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }

    public interface IAccountRepository : IAppReadRepository<Account>
    {

    }

    public interface IUserRepository : IAppReadRepository<User>
    {
        bool AuthenticateUser(string username, string password, out string message, out int isDisabled, out int userID);
    }
}
