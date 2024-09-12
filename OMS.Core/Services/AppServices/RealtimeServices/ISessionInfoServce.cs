using OMS.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.AppServices.RealtimeServices
{
    public interface ISessionInfoServce : IRealtimeService
    {
        SessionInfo GetSessionInfo();
    }
}
