using OMS.Core.Services;
using System.Windows.Threading;
using System;

namespace OMS.Services
{
    public class TimerService : ITimerService
    {
        private readonly DispatcherTimer _secondTimer;
        private readonly DispatcherTimer _minuteTimer;
        private readonly DispatcherTimer _hourTimer;

        private const int SecondInterval = 1000;  
        private const int MinuteInterval = 60000; 
        private const int HourInterval = 3600000; 

        public event EventHandler SecondTick;
        public event EventHandler MinuteTick;
        public event EventHandler HourTick;

        public TimerService()
        {
            _secondTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(SecondInterval)
            };
            _secondTimer.Tick += (s, e) => OnSecondTick();

            _minuteTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(MinuteInterval)
            };
            _minuteTimer.Tick += (s, e) => OnMinuteTick();

            _hourTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(HourInterval)
            };
            _hourTimer.Tick += (s, e) => OnHourTick();
        }

        public void Start()
        {
            _secondTimer.Start();
            _minuteTimer.Start();
            _hourTimer.Start();
        }
        public void Stop()
        {
            _secondTimer.Stop();
            _minuteTimer.Stop();
            _hourTimer.Stop();
        }

        protected virtual void OnSecondTick()
        {
            SecondTick?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnMinuteTick()
        {
            MinuteTick?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnHourTick()
        {
            HourTick?.Invoke(this, EventArgs.Empty);
        }
    }

}
