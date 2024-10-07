using OMS.Core.Models.User;
using System.Collections.Generic;

namespace OMS.DataAccess.Repositories.AppRepositories
{
    public interface IUserRepository 
    {
        IUser GetById(int id);
        IEnumerable<IUser> GetAll();
        IUser AuthenticateUser(UserCredentials credentials);
        IUser UpdateUser(IUser user);
    }
}
