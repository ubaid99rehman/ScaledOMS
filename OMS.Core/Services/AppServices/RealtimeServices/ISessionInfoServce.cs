using OMS.Core.Models.User;

namespace OMS.Core.Services.AppServices.RealtimeServices
{
    public interface ISessionInfoServce : IRealtimeService
    {
        ISessionInfo GetSessionInfo();
    }
}
