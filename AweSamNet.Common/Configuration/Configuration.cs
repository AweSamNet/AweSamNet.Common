using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
