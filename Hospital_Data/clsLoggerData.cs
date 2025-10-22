using System;
using System.Diagnostics;
using System.Threading;

namespace Hospital_Data
{
    public static class clsLoggerData
    {
        public static void Log(string Message, EventLogEntryType Type)
        {
            string source = "Hospital_System";
            string logName = "Application";

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, logName);
                Thread.Sleep(500); // Allow time for the source to be registered
            }

            EventLog.WriteEntry(source, Message, Type);
        }
    }
}
