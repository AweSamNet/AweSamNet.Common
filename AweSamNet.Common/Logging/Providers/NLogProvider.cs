using System;
//using NLog;

namespace AweSamNet.Common.Logging.Providers
{
    public class NLogProvider : ILogProvider
    {
        //private static NLog.Logger Default = LogManager.GetCurrentClassLogger();
        public NLogProvider()
        {
            throw new NotImplementedException();
        }

        public void Log(LogEntry entry)
        {
            throw new NotImplementedException();
            //switch (entry.Severity)
            //{
            //    case LoggingEventType.Verbose:
            //        Default.Trace(entry.Exception, entry.Message, entry.Args);
            //        break;
            //    case LoggingEventType.Debug:
            //        Default.Debug(entry.Exception, entry.Message, entry.Args);
            //        break;
            //    case LoggingEventType.Information:
            //        Default.Info(entry.Exception, entry.Message, entry.Args);
            //        break;
            //    case LoggingEventType.Warning:
            //        Default.Warn(entry.Exception, entry.Message, entry.Args);
            //        break;
            //    case LoggingEventType.Error:
            //        Default.Error(entry.Exception, entry.Message, entry.Args);
            //        break;
            //    case LoggingEventType.Fatal:
            //        Default.Fatal(entry.Exception, entry.Message, entry.Args);
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
        }
    }
}
