using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AweSamNet.Common.Configuration;
using SimpleInjector;
using AweSamNet.Common.Logging;

namespace AweSamNet.Common
{
    public interface IRegistry
    {
        IConfiguration Config { get; }
        ILogger Logger { get; }
        IMetrics Metrics { get; }
    }

    public class Registry : IRegistry
    {
        private static readonly Lazy<Container> _container = new Lazy<Container>(() =>
        {
            var container = new Container();

            container.Register<IRegistry, Registry>();
            container.Register<IConfiguration, Configuration.Configuration>();
            container.Register<ILogger, Logger>();
            container.Register<IConfigurationManager, ConfigurationManager>();
            container.Register<IMetrics, Metrics>();

            //get logger types to register
            var loggerTypes = Logging.Logger.GetLoggerTypes();
            if (loggerTypes.Any())
            {
                container.RegisterCollection<ILogProvider>(loggerTypes);
            }

            return container;
        });

        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IMetrics _metrics;

        public static Container Container
        {
            get { return _container.Value; }
        }

        public Registry(IConfiguration config, ILogger logger, IMetrics metrics)
        {
            _config = config;
            _logger = logger;
            _metrics = metrics;
        }

        public IConfiguration Config
        {
            get { return _config; }
        }

        public ILogger Logger
        {
            get { return _logger; }
        }

        public IMetrics Metrics
        {
            get { return _metrics; }
        }
    }
}
