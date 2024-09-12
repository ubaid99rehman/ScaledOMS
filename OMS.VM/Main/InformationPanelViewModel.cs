using DevExpress.Mvvm;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices.RealtimeServices;

namespace OMS.ViewModels
{
    public class InformationPanelViewModel : ViewModelBase
    {
        ISessionInfoServce SessionInfoServce { get;}
        
        public SessionInfo SessionInfo
        {
            get { return SessionInfoServce.GetSessionInfo(); }
        }

        public InformationPanelViewModel(ISessionInfoServce infoServce) 
        {
            SessionInfoServce = infoServce;
            SessionInfoServce.StartSession();
        }
    }
}
