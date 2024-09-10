using OMS.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.RealtimeServices
{
    public interface ISessionInfoServce : IRealtimeService
    {
        void StartSession();
        SessionInfo GetSessionInfo();
    }
}
