using OMS.Core.Services;
using System.Windows.Threading;
using System;

namespace OMS.Services
{
    public class TimerService : ITimerService
    {
        private readonly DispatcherTimer _timer;
        private const int TickInterval = 600;
        public event EventHandler Tick;

        public TimerService()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(TickInterval)
            };
            _timer.Tick += (s, e) => OnTick();
        }
        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();
        protected virtual void OnTick()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }
}
