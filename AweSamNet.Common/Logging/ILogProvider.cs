using System;

namespace AweSamNet.Common.Logging
{
    public interface ILogProvider
    {
        void Log(LogEntry entry);
        Action<LoggingEventType> LogLevelSetter { get; }
    }
}
