﻿using System;
using AweSamNet.Common.Logging;

namespace AweSamNet.Common.Configuration
{
    public class Configuration : IConfiguration
    {
        protected IConfigurationManager ConfigurationManager { get; private set; }
        public Configuration(IConfigurationManager configurationManager)
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

            _geoLookupGeoNamesUser = new Lazy<string>(() =>
                ConfigurationManager.AppSettings["GeoLookup.GeoNamesUser"]);

            _geoLookupCacheExpiration = new Lazy<TimeSpan>(() =>
            {
                int minutes;
                if (!int.TryParse(ConfigurationManager.AppSettings["GeoLookup.CacheExpirationMinutes"], out minutes))
                {
                    return TimeSpan.FromDays(7);
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

        private readonly Lazy<string> _geoLookupGeoNamesUser;
        public string GeoLookupGeoNamesUser
        {
            get { return _geoLookupGeoNamesUser.Value; }
        }

        private readonly Lazy<TimeSpan> _geoLookupCacheExpiration;
        public TimeSpan GeoLookupCacheExpiration
        {
            get { return _geoLookupCacheExpiration.Value; }
        }
    }
}
