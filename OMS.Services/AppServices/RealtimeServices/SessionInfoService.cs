using OMS.Core.Models.User;
using OMS.Core.Services.AppServices.RealtimeServices;
using System;
using System.Windows.Threading;

namespace OMS.Services.AppServices
{
    public class SessionInfoService : ISessionInfoServce
    {
        private SessionInfo _sessionInfo;
        private readonly Random _random;
        public const int Tick = 500;
        readonly DispatcherTimer updateTimer;

        public SessionInfoService()
        {
            _random = new Random();
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }

        public void StartSession()
        {
            _sessionInfo = new SessionInfo();
            updateTimer.Start();
        }

        public void Refresh(object sender, EventArgs e)
        {
            TimeSpan sessionTimeSpan = DateTime.Now - _sessionInfo.LoginTime;
            _sessionInfo.Ping = _random.Next(200, 801).ToString() + "ms";
            _sessionInfo.SessionTime = $"{sessionTimeSpan.Hours}h {sessionTimeSpan.Minutes}m {sessionTimeSpan.Seconds}s";
        }

        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }

        public SessionInfo GetSessionInfo()
        {
            if(_sessionInfo == null)
            {
                return new SessionInfo();
            }
            return _sessionInfo;
        }
    }
}
