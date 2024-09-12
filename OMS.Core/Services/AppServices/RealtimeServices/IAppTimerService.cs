using OMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.AppServices.RealtimeServices
{
    public interface IAppTimerService : IRealtimeService
    {
        AppTime GetCurrentDateTime();
    }
}
