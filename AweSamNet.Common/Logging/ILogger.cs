using System;

namespace AweSamNet.Common.Logging
{
    public interface ILogger
    {
        void Debug(string message, params object[] args);
        void Debug(string message, Exception ex, params object[] args);
        void Info(string message, params object[] args);
        void Info(string message, Exception ex, params object[] args);
        void Warn(string message, params object[] args);
        void Warn(string message, Exception ex, params object[] args);
        void Error(string message, params object[] args);
        void Error(string message, Exception ex, params object[] args);
        void Fatal(string message, params object[] args);
        void Fatal(string message, Exception ex, params object[] args);
    }
}