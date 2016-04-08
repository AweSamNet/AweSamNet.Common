using System;
using AweSamNet.Common.Logging;

namespace AweSamNet.Common.Configuration
{
    public class Configuration : IConfiguration
    {
        protected ConfigurationManager ConfigurationManager { get; private set; }
        public Configuration(ConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
            _loggerConfigSection = new Lazy<ILoggerConfigSection>(() => 
                ConfigurationManager.GetSection("LoggerSection") as LoggerConfigSection);

            _application = new Lazy<string>(() =>
                ConfigurationManager.AppSettings["appName"]);

            _environment = new Lazy<string>(() =>
                ConfigurationManager.AppSettings["environment"]);

            _defaultCacheExpiration = new Lazy<TimeSpan>(() =>
            {
                int minutes;
                if (!int.TryParse(ConfigurationManager.AppSettings["Cache.DefaultExpirationMinutes"], out minutes))
                {
                    minutes = 20;
                }

                return new TimeSpan(0, minutes, 0);
            });
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

        private readonly Lazy<TimeSpan> _defaultCacheExpiration;
        public TimeSpan DefaultCacheExpiration
        {
            get { return _defaultCacheExpiration.Value; }
        }

    }
}
