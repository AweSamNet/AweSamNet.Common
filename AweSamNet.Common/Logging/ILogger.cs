using System;

namespace AweSamNet.Common.Logging
{
    public interface ILogger
    {
        void Verbose(string message, params object[] args);
        void Verbose(Exception ex, string message, params object[] args);
        void Debug(string message, params object[] args);
        void Debug(Exception ex, string message, params object[] args);
        void Info(string message, params object[] args);
        void Info(Exception ex, string message, params object[] args);
        void Warn(string message, params object[] args);
        void Warn(Exception ex, string message, params object[] args);
        void Error(string message, params object[] args);
        void Error(Exception ex, string message, params object[] args);
        void Fatal(string message, params object[] args);
        void Fatal(Exception ex, string message, params object[] args);
    }
}