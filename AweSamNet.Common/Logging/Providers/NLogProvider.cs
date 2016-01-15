using System;
using NLog;

namespace AweSamNet.Common.Logging.Providers
{
    public class NLogProvider : ILogProvider
    {
        private static NLog.Logger Default = LogManager.GetCurrentClassLogger();

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Debug:
                    Default.Debug(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Information:
                    Default.Info(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Warning:
                    Default.Warn(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Error:
                    Default.Error(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Fatal:
                    Default.Fatal(entry.Exception, entry.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
