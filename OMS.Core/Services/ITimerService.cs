using System;

namespace OMS.Core.Services
{
    public interface ITimerService
    {
        event EventHandler SecondTick;
        event EventHandler MinuteTick;
        event EventHandler HourTick;

        void Start();
        void Stop();
    }

}
