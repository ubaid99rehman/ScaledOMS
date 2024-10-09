using OMS.Core.Models;
using OMS.Core.Models.App;
using OMS.Core.Services;
using OMS.Core.Services.AppServices.RealtimeServices;
using System;


namespace OMS.Services.AppServices
{
    public class AppTimerService : IAppTimerService
    {
        public IAppTime CurrentTime { get; }
        
        //Constructor
        public AppTimerService(ITimerService timerService)
        {
            CurrentTime = new AppTime();
            timerService.Tick += OnTimerTick;
            timerService.Start();
        }
        
        public IAppTime GetCurrentDateTime()
        {
            return CurrentTime;
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            CurrentTime.CurrentTime = DateTime.Now;
        }
    }

}
