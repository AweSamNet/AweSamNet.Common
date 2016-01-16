using System;
using System.Diagnostics;
using System.IO;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace AweSamNet.Common.Logging.Providers
{
    public class SerilogProvider : ILogProvider
    {
        public static TextWriter SelfLog = Console.Out;
        static SerilogProvider()
        {
            Serilog.Debugging.SelfLog.Out = SelfLog;
            Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .ReadFrom.AppSettings()
                .MinimumLevel.ControlledBy(_loggingLevelSwitch)
                .WriteTo.ColoredConsole()
                .CreateLogger();

            //Serilog.Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Is(LogEventLevel.Information)
            //    .Enrich.WithMachineName()
            //    .Enrich.WithThreadId()
            //    .Enrich.FromLogContext()
            //    .WriteTo.RollingFile("textwriter-{Date}.log",
            //        outputTemplate:
            //            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} level={Level} appName={app} environment={env} version={version} machine={MachineName} thread={ThreadId} {errorContext} {context} {Message}{NewLine}{Exception}",
            //        fileSizeLimitBytes: null)
            //    .WriteTo.ColoredConsole()
            //    .CreateLogger();
        }

        private static readonly LoggingLevelSwitch _loggingLevelSwitch = new LoggingLevelSwitch((LogEventLevel) Logger.MinimumLogLevel);

        private readonly Action<LoggingEventType> _logLevelSetter = (x) => _loggingLevelSwitch.MinimumLevel = (LogEventLevel)x;
        public Action<LoggingEventType> LogLevelSetter
        {
            get
            {
                return _logLevelSetter;
            }
        }
            
                        
        //    LoggingLevel()
        //{
        //    _loggingLevelSwitch.MinimumLevel = (LogEventLevel) eventType;
        //}

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Verbose:
                    Serilog.Log.Logger.Verbose(entry.Exception, entry.Message, entry.Args);
                    break;
                case LoggingEventType.Debug:
                    Serilog.Log.Logger.Debug(entry.Exception, entry.Message, entry.Args);
                    break;
                case LoggingEventType.Information:
                    Serilog.Log.Logger.Information(entry.Exception, entry.Message, entry.Args);
                    break;
                case LoggingEventType.Warning:
                    Serilog.Log.Logger.Warning(entry.Exception, entry.Message, entry.Args);
                    break;
                case LoggingEventType.Error:
                    Serilog.Log.Logger.Error(entry.Exception, entry.Message, entry.Args);
                    break;
                case LoggingEventType.Fatal:
                    Serilog.Log.Logger.Fatal(entry.Exception, entry.Message, entry.Args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
