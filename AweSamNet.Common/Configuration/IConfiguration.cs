using AweSamNet.Common.Logging;

namespace AweSamNet.Common.Configuration
{
    public interface IConfiguration
    {
        ILoggerConfigSection LoggerConfigSection { get; }
        string Application { get; }
        string Environment { get; }
    }
}