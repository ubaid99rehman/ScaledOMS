using OMS.Core.Models.User;

namespace OMS.DataAccess.Repositories.AppRepositories
{
    public interface IUserRepository 
    {
        IUser GetById(int id);
        bool AuthenticateUser(string username, string password, out string message, out int isDisabled, out int userID);
        bool UpdateUser(IUser user);
    }
}
