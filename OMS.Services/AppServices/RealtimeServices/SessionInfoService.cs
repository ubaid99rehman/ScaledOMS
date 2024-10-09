using OMS.Core.Models.User;
using OMS.Core.Services;
using OMS.Core.Services.AppServices.RealtimeServices;
using System;
using System.Windows.Threading;

namespace OMS.Services.AppServices
{
    public class SessionInfoService : ISessionInfoServce
    {
        private ISessionInfo _sessionInfo;
        private readonly Random _random;

        //Constructor
        public SessionInfoService(ITimerService timerService)
        {
            _random = new Random();
            _sessionInfo = new SessionInfo();
            timerService.Tick += OnTimerTick;
            timerService.Start();
        }

        public ISessionInfo GetSessionInfo()
        {
            if (_sessionInfo == null)
            {
                return new SessionInfo();
            }
            return _sessionInfo;
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_sessionInfo == null) return;

            TimeSpan sessionTimeSpan = DateTime.Now - _sessionInfo.LoginTime;
            _sessionInfo.Ping = $"{_random.Next(200, 801)}ms";
            _sessionInfo.SessionTime = $"{sessionTimeSpan.Hours}h {sessionTimeSpan.Minutes}m {sessionTimeSpan.Seconds}s";
        }
    }

}
