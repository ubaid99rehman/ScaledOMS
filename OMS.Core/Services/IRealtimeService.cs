using System;

namespace OMS.Core.Services
{
    public interface ITimerService
    {
        event EventHandler Tick;
        void Start();
        void Stop();
    }
}
