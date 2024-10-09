using OMS.Core.Models.App;

namespace OMS.Core.Services.AppServices.RealtimeServices
{
    public interface IAppTimerService
    {
        IAppTime GetCurrentDateTime();
    }
}
