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
        public string Key { get; set; }
        public string MethodName { get; set; }
        public string FilePath { get; set; }
        public int LineNumber { get; set; }
        public System.Diagnostics.Stopwatch Sw { get; set; }
        public DateTime StopwatchStart { get; set; }
        public DateTime StopwatchStop { get; set; }

        internal Stopwatch(ILogger logger, string key, string methodName, string filePath, int lineNumber)
        {
            _logger = logger;
            Key = key;
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
                "Key '{0}' | Method '{1}' | File '{2}' | Line Number: {3}  | Started: {4} | Ended {5} | Elapsed {6} ms",
                Key,
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
            string key = null,
            [CallerMemberName] string callingMethodName = null,
            [CallerFilePath] string callingFilePath = null,
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
            string key,
            [CallerMemberName] string callingMethodName = null,
            [CallerFilePath] string callingFilePath = null,
            [CallerLineNumber] int callingLineNumber = 0)
        {
            return new Stopwatch(_logger, key, callingMethodName, callingFilePath, callingLineNumber);
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