using OMS.Core.Models;
using OMS.Core.Models.App;
using OMS.Core.Services.AppServices.RealtimeServices;
using System;
using System.Windows.Threading;


namespace OMS.Services.AppServices
{
    public class AppTimerService : IAppTimerService
    {
        public IAppTime CurrentTime;
        public const int Tick = 500;
        readonly DispatcherTimer updateTimer;
        readonly Random _random;

        //Constructor
        public AppTimerService() 
        {
            CurrentTime = new AppTime();
            _random = new Random();
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }
        
        //Public Access Methods
        public void Refresh(object sender, EventArgs e)
        {
            CurrentTime.CurrentTime = DateTime.Now;
        }
        public void StartSession() 
        {
            updateTimer.Start();
        }
        public IAppTime GetCurrentDateTime()
        {
            return CurrentTime;
        }
        //Private Method
        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }
    }
}
