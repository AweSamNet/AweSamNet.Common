using System;
using System.Linq;
using System.Collections.Generic;
using AweSamNet.Common.Configuration;
using AweSamNet.Common.Logging;
using AweSamNet.Common.Logging.Providers;
using Moq;
using NUnit.Framework;

namespace AweSamNet.Common.Tests.Logging
{
    [TestFixture]
    public class Logger
    {
        [Test]
        public void logger_types_can_be_loaded_from_config_and_instantiated_into_loggers()
        {
            //assemble
            var nLogProviderType = typeof(NLogProvider);
            var nLogElement = new LoggerConfigElement
            {
                Assembly = nLogProviderType.Assembly.FullName,
                FullName = nLogProviderType.FullName
            };

            var serilogProviderType = typeof(SerilogProvider);
            var serilogElement = new LoggerConfigElement
            {
                Assembly = serilogProviderType.Assembly.FullName,
                FullName = serilogProviderType.FullName
            };

            var loggerConfigElements = new List<LoggerConfigElement> { nLogElement, serilogElement };

            var loggerConfigElementCollection = new Mock<LoggerConfigElementCollection>();
            loggerConfigElementCollection
                .Setup(x => x.GetEnumerator())
                .Returns(loggerConfigElements.GetEnumerator());

            var loggerConfigSection = new Mock<ILoggerConfigSection>();
            loggerConfigSection.Setup(x => x.AllValues)
                .Returns(loggerConfigElementCollection.Object);

            var config = new Mock<IConfiguration>();
            config.Setup(x => x.LoggerConfigSection)
                .Returns(loggerConfigSection.Object);

            //act
            //get the logger types
            var providerTypes = Common.Logging.Logger.GetLoggerTypes(loggerConfigSection.Object);

            var loggers = new List<ILogProvider>();
            foreach (var providerType in providerTypes)
            {
                object o = Activator.CreateInstance(providerType);
                loggers.Add(o as ILogProvider);
            }

            //assert
            Assert.IsNotEmpty(providerTypes, "No logger providerTypes found.");
            Assert.IsTrue(loggerConfigElements.Count == providerTypes.Count,
                "loggerConfigElements.Count: {0}, providerTypes.Count: {1}", loggerConfigElements.Count, providerTypes.Count);
            Assert.IsTrue(providerTypes.Count == loggers.Count,
                "providerTypes.Count: {0}, loggers.Count: {1}", providerTypes.Count, loggers.Count);

            Assert.IsFalse(loggers.Any(x => x == null));

        }

        private readonly object[] _providerData =
        {
            new object[] {new ILogProvider[] {new SerilogProvider()}},
            new object[] {new ILogProvider[] {new NLogProvider()}},
            new object[] {new ILogProvider[] {new SerilogProvider(), new NLogProvider()}},
        };

        [Test, TestCaseSource("_providerData")]
        public void should_not_throw_exception_at_all_levels_of_logging(ILogProvider[] providers)
        {
            //assemble
            ILogger logger = new AweSamNet.Common.Logging.Logger(providers);
            const string message = "This is a {0} message.";
            Exception ex = new Exception("Test using exceptions.");

            //act
            logger.Verbose(message, LoggingEventType.Verbose);
            logger.Verbose(ex, message, LoggingEventType.Verbose);

            logger.Verbose(message, LoggingEventType.Debug);
            logger.Verbose(ex, message, LoggingEventType.Debug);

            logger.Verbose(message, LoggingEventType.Information);
            logger.Verbose(ex, message, LoggingEventType.Information);

            logger.Verbose(message, LoggingEventType.Warning);
            logger.Verbose(ex, message, LoggingEventType.Warning);

            logger.Verbose(message, LoggingEventType.Error);
            logger.Verbose(ex, message, LoggingEventType.Error);

            logger.Verbose(message, LoggingEventType.Fatal);
            logger.Verbose(ex, message, LoggingEventType.Fatal);

            //assert
            //nothing to do
        }
    }
}