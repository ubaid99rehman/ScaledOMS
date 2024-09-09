
using OMS.Core.Core.Models.User;
using OMS.Core.Models.User;

namespace OMS.Core.Services.AppServices
{
    public interface IAuthService
    {
        User Authenticate(UserCredentials credentials);
    }
}
