using System;

namespace AweSamNet.Common.Logging
{
    // http://stackoverflow.com/questions/5646820/logger-wrapper-best-practice/5646876
    // Immutable DTO that contains the log information.
    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;
        public readonly object[] Args;

        public LogEntry(LoggingEventType severity, string message, Exception exception = null, params object[] args)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("message");
            }

            Severity = severity;
            Message = message;
            Args = args;
            Exception = exception;
        }
    }
}