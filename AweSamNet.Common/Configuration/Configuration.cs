using AweSamNet.Common.Logging;

namespace AweSamNet.Common.Configuration
{
    public class Configuration : IConfiguration
    {
        private readonly ConfigurationManager _configurationManager;
        public Configuration(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }
        public ILoggerConfigSection LoggerConfigSection
        {
            get { return _configurationManager.GetSection("LoggerSection") as LoggerConfigSection; }
        }
    }
}
