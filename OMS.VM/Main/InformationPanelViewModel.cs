﻿using DevExpress.Mvvm;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices.RealtimeServices;

namespace OMS.ViewModels
{
    public class InformationPanelViewModel : ViewModelBase
    {
        //Services
        ISessionInfoServce SessionInfoServce { get;}
        
        //Public Session Data Member
        public ISessionInfo SessionInfo
        {
            get { return SessionInfoServce.GetSessionInfo(); }
        }

        //Constructor
        public InformationPanelViewModel(ISessionInfoServce infoServce) 
        {
            SessionInfoServce = infoServce;
        }
    }
}
