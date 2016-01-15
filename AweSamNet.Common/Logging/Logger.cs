using System;
using System.Collections.Generic;
using AweSamNet.Common.Configuration;

namespace AweSamNet.Common.Logging
{
    public class Logger : ILogger
    {
        private readonly IEnumerable<ILogProvider> _logProviders;

        public Logger(IEnumerable<ILogProvider> logProviders)
        {
            _logProviders = logProviders;
        }

        public static List<Type> GetLoggerTypes(IConfiguration config)
        {
            var loggerTypes = new List<Type>();
            foreach (var loggerConfig in config.LoggerConfigSection.AllValues)
            {
                if (!loggerConfig.Enabled) continue;
                loggerTypes.Add(Type.GetType((loggerConfig.FullName + ", " + loggerConfig.Assembly)));
            }
            return loggerTypes;
        }

        #region Verbose
        public void Verbose(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Verbose, message, args);
        }

        public void Verbose(string message, Exception ex, params object[] args)
        {
            WriteLog(LoggingEventType.Verbose, message, ex, args);
        }
        #endregion Verbose

        #region Debug
        public void Debug(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Debug, message, args);
        }

        public void Debug(string message, Exception ex, params object[] args)
        {
            WriteLog(LoggingEventType.Debug, message, ex, args);
        }
        #endregion Debug

        #region Info
        public void Info(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Information, message, args);
        }

        public void Info(string message, Exception ex, params object[] args)
        {
            WriteLog(LoggingEventType.Information, message, ex, args);
        }
        #endregion Info

        #region Warn
        public void Warn(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Warning, message, args);
        }

        public void Warn(string message, Exception ex, params object[] args)
        {
            WriteLog(LoggingEventType.Warning, message, ex, args);
        }
        #endregion Warn

        #region Error
        public void Error(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Error, message, args);
        }

        public void Error(string message, Exception ex, params object[] args)
        {
            WriteLog(LoggingEventType.Error, message, ex, args);
        }
        #endregion Error

        #region Fatal
        public void Fatal(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Fatal, message, args);
        }

        public void Fatal(string message, Exception ex, params object[] args)
        {
            WriteLog(LoggingEventType.Fatal, message, ex, args);
        }
        #endregion Fatal

        #region WriteLog
        private void WriteLog(LoggingEventType type, string message, params object[] args)
        {
            WriteLog(type, message, null, args);
        }

        private void WriteLog(LoggingEventType type, string message, Exception ex = null, params object[] args)
        {
            foreach (var log in _logProviders)
            {
                log.Log(new LogEntry(type, message, ex, args));
            }
        }
        #endregion WriteLog
    }
}