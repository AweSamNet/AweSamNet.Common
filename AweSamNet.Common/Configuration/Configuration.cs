using System;
using AweSamNet.Common.Logging;

namespace AweSamNet.Common.Configuration
{
    public class Configuration : IConfiguration
    {
        private readonly ConfigurationManager _configurationManager;
        public Configuration(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
            _loggerConfigSection = new Lazy<ILoggerConfigSection>(() => 
                _configurationManager.GetSection("LoggerSection") as LoggerConfigSection);

            _application = new Lazy<string>(() =>
                _configurationManager.AppSettings["appName"]);

            _environment = new Lazy<string>(() =>
                _configurationManager.AppSettings["environment"]);
        }

        private readonly Lazy<ILoggerConfigSection> _loggerConfigSection;
        public ILoggerConfigSection LoggerConfigSection
        {
            get { return _loggerConfigSection.Value; }
        }

        private readonly Lazy<string> _application;
        public string Application
        {
            get { return _application.Value; }
        }

        private readonly Lazy<string> _environment;
        public string Environment
        {
            get { return _environment.Value; }
        }
    }
}
