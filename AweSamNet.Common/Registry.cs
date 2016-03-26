using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AweSamNet.Common.Caching;
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
        ICache Cache { get; }
    }

    public class Registry : IRegistry
    {
        private static readonly Lazy<Container> _container = new Lazy<Container>(() =>
        {
            var container = new Container();

            if (_lifeStyle != null)
            {
                container.Options.DefaultScopedLifestyle = _lifeStyle;
            }

            container.Options.AllowOverridingRegistrations = true;

            container.Register<IRegistry, Registry>();
            container.Register<IConfiguration, Configuration.Configuration>();

            //get logger types to register
            var loggerTypes = Logging.Logger.GetLoggerTypes();
            if (loggerTypes.Any())
            {
                container.RegisterCollection<ILogProvider>(loggerTypes);
            }

            container.Register<ILogger, Logger>();
            container.Register<IConfigurationManager, ConfigurationManager>();
            container.Register<IMetrics, Metrics>();
            container.Register<ICache, MemoryCache>();

            return container;
        });

        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IMetrics _metrics;
        private static ScopedLifestyle _lifeStyle = null;
        private readonly ICache _cache;

        public static Container GetContainer(ScopedLifestyle lifeStyle = null)
        {
            //if there is already a lifestyle set, this will not reset the lifeStyle of previously registered classes
            if (lifeStyle != null)
            {
                _lifeStyle = lifeStyle;
            }

            return _container.Value;
        }

        public Registry(IConfiguration config, ILogger logger, IMetrics metrics, ICache cache)
        {
            _config = config;
            _logger = logger;
            _metrics = metrics;
            _cache = cache;
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

        public ICache Cache
        {
            get { return _cache; }
        }
    }
}
