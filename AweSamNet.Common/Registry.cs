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
    }

    public class Registry : IRegistry
    {
        private static readonly Lazy<Container> _container = new Lazy<Container>(() =>
        {
            var container = new Container();

            container.Register<IConfiguration, Configuration.Configuration>(Lifestyle.Singleton);
            container.Register<ILogger, Logger>(Lifestyle.Singleton);
            
            //get logger types to register
            var loggerTypes = Logging.Logger.GetLoggerTypes(container.GetInstance<IConfiguration>());
            container.RegisterCollection<ILogger>(loggerTypes);


            return container;
        });

        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public static Container Container
        {
            get { return _container.Value; }
        }

        public Registry(IConfiguration config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public IConfiguration Config
        {
            get { return _config; }
        }

        public ILogger Logger
        {
            get { return _logger; }
        }
    }
}
