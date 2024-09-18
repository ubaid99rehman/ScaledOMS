using System;
using System.Diagnostics;

namespace OMS.Logging
{
    public static class LogHelper
    {
        private static TraceSource _traceSource = new TraceSource("TcpServerLog");

        public static void LogInfo(string message)
        {
            string logMessage = $"[{DateTime.Now}] INFO: {message}";
            _traceSource.TraceInformation(logMessage);
            _traceSource.Flush();
        }

        public static void LogError(string message, Exception ex)
        {
            string logMessage = $"[{DateTime.Now}] ERROR: {message} Exception: {ex.Message}";
            _traceSource.TraceEvent(TraceEventType.Error, 0, logMessage);
            _traceSource.Flush();
        }
    }
}
