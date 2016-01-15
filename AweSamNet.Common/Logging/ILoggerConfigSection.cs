using System.Configuration;

namespace AweSamNet.Common.Logging
{
    public interface ILoggerConfigSection
    {
        [ConfigurationProperty("loggerCollection")]
        ILoggerConfigElementCollection AllValues { get; }
    }
}