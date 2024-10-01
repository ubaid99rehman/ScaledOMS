using OMS.Core.Models.User;

namespace OMS.Core.Services.AppServices
{
    public interface IAuthService
    {
        IUser Authenticate(UserCredentials credentials);
    }
}
