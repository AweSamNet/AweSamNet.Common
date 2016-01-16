using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AweSamNet.Common.Logging
{
    public class Logger : ILogger
    {
        private readonly IEnumerable<ILogProvider> _logProviders;

        /// <summary>
        /// This logger should only be used in a static context as DI is not available statically.  
        /// Standard practice is to rely on DI for the logger instance.
        /// </summary>
        public static Logger Default { get; private set; }

        private static readonly object lockSync = new object();
        public Logger(IEnumerable<ILogProvider> logProviders)
        {
            _logProviders = logProviders;

            if (Default == null)
            {
                lock (lockSync)
                {
                    if (Default == null)
                    {
                        Default = this;
                    }
                }
            }
        }

        public static List<Type> GetLoggerTypes()
        {
            var loggerSection = LoggerConfigSection.GetConfigSection();
            return GetLoggerTypes(loggerSection);
        }

        private static LoggingEventType? _minimumLogLevel;
        public static LoggingEventType MinimumLogLevel
        {
            get
            {
                if (!_minimumLogLevel.HasValue)
                {
                    LoggingEventType type;
                    if (!Enum.TryParse(ConfigurationManager.AppSettings["minimumLogLevel"], out type))
                    {
                        type = LoggingEventType.Information;
                    }
                    _minimumLogLevel = type;
                }
                return _minimumLogLevel.Value;
            }
            set
            {
                _minimumLogLevel = value;
                if (Default != null)
                {
                    foreach (var logger in Default._logProviders.Where(logger => logger.LogLevelSetter != null))
                    {
                        logger.LogLevelSetter(value);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the passed eventType to the configuration file for this project.
        /// </summary>
        /// <param name="eventType"></param>
        public static void SetDefaultLogLevel(LoggingEventType eventType)
        {
            Configuration.ConfigurationManager.AddOrUpdateAppSettings("minimumLogLevel", eventType.ToString());
        }

        public static List<Type> GetLoggerTypes(ILoggerConfigSection loggerSection)
        {
            var loggerTypes = new List<Type>();
            if (loggerSection == null) return loggerTypes;
            foreach (var loggerConfig in loggerSection.AllValues)
            {
                if (!loggerConfig.Enabled) continue;
                loggerTypes.Add(Type.GetType(loggerConfig.FullName + ", " + loggerConfig.Assembly));
            }
            return loggerTypes;
        }

        #region Verbose
        public void Verbose(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Verbose, message, args);
        }

        public void Verbose(Exception ex, string message, params object[] args)
        {
            WriteLog(LoggingEventType.Verbose, message, ex, args);
        }
        #endregion Verbose

        #region Debug
        public void Debug(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Debug, message, args);
        }

        public void Debug(Exception ex, string message, params object[] args)
        {
            WriteLog(LoggingEventType.Debug, message, ex, args);
        }
        #endregion Debug

        #region Info
        public void Info(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Information, message, args);
        }

        public void Info(Exception ex, string message, params object[] args)
        {
            WriteLog(LoggingEventType.Information, message, ex, args);
        }
        #endregion Info

        #region Warn
        public void Warn(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Warning, message, args);
        }

        public void Warn(Exception ex, string message, params object[] args)
        {
            WriteLog(LoggingEventType.Warning, message, ex, args);
        }
        #endregion Warn

        #region Error
        public void Error(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Error, message, args);
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            WriteLog(LoggingEventType.Error, message, ex, args);
        }
        #endregion Error

        #region Fatal
        public void Fatal(string message, params object[] args)
        {
            WriteLog(LoggingEventType.Fatal, message, args);
        }

        public void Fatal(Exception ex, string message, params object[] args)
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