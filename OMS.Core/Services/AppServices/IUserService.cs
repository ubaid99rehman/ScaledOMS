using OMS.Core.Models.User;

namespace OMS.Core.Services.AppServices
{
    public interface IUserService
    {
        bool UpdateUser(IUser user);
        IUser GetUser();
    }

}
