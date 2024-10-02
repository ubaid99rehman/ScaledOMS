using OMS.Core.Logging;
using System;
using System.Diagnostics;

namespace OMS.Logging
{
    public class LogHelper : ILogHelper
    {
        //TraceSource Instance
        private TraceSource _traceSource; 
        
        public LogHelper() 
        {
            _traceSource = new TraceSource("OMSClientLogs");
        }

        //Logging Methods
        public void LogInfo(string message)
        {
            string logMessage = $"[{DateTime.Now}] INFO: {message}";
            _traceSource.TraceInformation(logMessage);
            _traceSource.Flush();
        }
        public void LogError(string message, Exception ex)
        {
            string logMessage = $"[{DateTime.Now}] ERROR: {message} Exception: {ex.Message}";
            _traceSource.TraceEvent(TraceEventType.Error, 0, logMessage);
            _traceSource.Flush();
        }
    }
}
