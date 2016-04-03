using System;
using AweSamNet.Common.Logging;

namespace AweSamNet.Common.Tests.Logging.Providers
{
    public class TestLogProvider1 : ILogProvider
    {

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Verbose:
                case LoggingEventType.Debug:
                case LoggingEventType.Information:
                case LoggingEventType.Warning:
                case LoggingEventType.Error:
                case LoggingEventType.Fatal:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Action<LoggingEventType> LogLevelSetter
        {
            get { return null; }
        }
    }

    public class TestLogProvider2 : ILogProvider
    {

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Verbose:
                case LoggingEventType.Debug:
                case LoggingEventType.Information:
                case LoggingEventType.Warning:
                case LoggingEventType.Error:
                case LoggingEventType.Fatal:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Action<LoggingEventType> LogLevelSetter
        {
            get { return null; }
        }
    }
}
