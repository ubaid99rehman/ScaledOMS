using OMS.Core.Models.User;

namespace OMS.Core.Services.AppServices
{
    public interface IUserService :IAppService<IUser>, IReadonlyService<IUser>
    {
        IUser SetUser(IUser user);
        IUser GetUser();
        bool LoadUsers();
    }
}
