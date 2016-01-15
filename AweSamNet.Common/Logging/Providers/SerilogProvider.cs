using System;
using Serilog;

namespace AweSamNet.Common.Logging.Providers
{
    public class SerilogProvider : ILogProvider
    {
        static SerilogProvider()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();             
        }

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
