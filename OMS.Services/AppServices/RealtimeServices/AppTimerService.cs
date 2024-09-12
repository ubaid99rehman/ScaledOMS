using OMS.Core.Models;
using OMS.Core.Services.AppServices.RealtimeServices;
using System;
using System.Windows.Threading;


namespace OMS.Services.AppServices
{
    public class AppTimerService : IAppTimerService
    {
        public AppTime CurrentTime;

        public const int Tick = 500;
        readonly DispatcherTimer updateTimer;
        readonly Random _random;

        public void Refresh(object sender, EventArgs e)
        {
            CurrentTime.CurrentTime = DateTime.Now;
        }

        public AppTimerService() 
        {
            CurrentTime = new AppTime();
            _random = new Random();
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }

        public void StartSession() 
        {
            updateTimer.Start();
        }

        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }

        public AppTime GetCurrentDateTime()
        {
            return CurrentTime;
        }

    }
}
