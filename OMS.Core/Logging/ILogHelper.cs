using System;

namespace OMS.Core.Logging
{
    public interface ILogHelper
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex);
    }
}
