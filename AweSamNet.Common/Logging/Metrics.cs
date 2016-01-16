using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace AweSamNet.Common.Logging
{
    //http://lancelarsen.com/stopwatchlog-easy-method-timing-in-c/
    /// <summary>
    ///  Collects information about executing method and logs metrics 
    ///     when the stopwatch log is disposed.
    /// </summary>
    /// <example>
    ///   using (Metrics.Start())
    ///   {
    ///     // Code here...
    ///   }
    /// </example>
    public sealed class Stopwatch : IDisposable
    {
        private readonly ILogger _logger;
        public string MethodName { get; set; }
        public string FilePath { get; set; }
        public int LineNumber { get; set; }
        public System.Diagnostics.Stopwatch Sw { get; set; }
        public DateTime StopwatchStart { get; set; }
        public DateTime StopwatchStop { get; set; }

        internal Stopwatch(ILogger logger, string methodName, string filePath, int lineNumber)
        {
            _logger = logger;
            MethodName = methodName;
            FilePath = filePath;
            LineNumber = lineNumber;
            StopwatchStart = DateTime.UtcNow;
            Sw = System.Diagnostics.Stopwatch.StartNew();
        }

        void IDisposable.Dispose()
        {
            Sw.Stop();
            StopwatchStop = DateTime.UtcNow;
            LogMetrics();
        }

        private void LogMetrics()
        {
            _logger.Info(
                "Method '{0}' | File '{1}' | Line Number: {2}  | Started: {3} | Ended {4} | Elapsed {5} ms",
                MethodName,
                FilePath,
                LineNumber,
                StopwatchStart.ToDateTimeHighPrecision(),
                StopwatchStop.ToDateTimeHighPrecision(),
                Sw.ElapsedMilliseconds);
        }
    }

    public interface IMetrics
    {
        Stopwatch Start(
            [CallerMemberName] string callingMethodName = "",
            [CallerFilePath] string callingFilePath = "",
            [CallerLineNumber] int callingLineNumber = 0);
    }

    public class Metrics : IMetrics
    {
        private readonly ILogger _logger;
        public Metrics(ILogger logger)
        {
            _logger = logger;
        }

        public Stopwatch Start(
            [CallerMemberName] string callingMethodName = "",
            [CallerFilePath] string callingFilePath = "",
            [CallerLineNumber] int callingLineNumber = 0)
        {
            return new Stopwatch(_logger, callingMethodName, callingFilePath, callingLineNumber);
        }
    }

    public static class DateTimeExtentions
    {
        public static string ToDateTimeHighPrecision(this DateTime source)
        {
            return source.ToString("MM/dd/yyyy h:mm:ss.fff tt");
        }
    }
}