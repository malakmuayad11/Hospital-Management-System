using Hospital_Data;
using System.Diagnostics;

namespace Hospital_Business
{
    public class clsLogger
    {
        public static void Log(string Message, EventLogEntryType Type) => clsLoggerData.Log(Message, Type);
    }
}
