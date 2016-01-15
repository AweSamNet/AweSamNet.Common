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
            var nLogProviderType = typeof (NLogProvider);
            var nLogElement = new LoggerConfigElement();
            nLogElement.Assembly = nLogProviderType.Assembly.FullName;
            nLogElement.FullName = nLogProviderType.FullName;

            var serilogProviderType = typeof(SerilogProvider);
            var serilogElement = new LoggerConfigElement();
            serilogElement.Assembly = serilogProviderType.Assembly.FullName;
            serilogElement.FullName = serilogProviderType.FullName;

            var loggerConfigElements = new List<LoggerConfigElement> { nLogElement, serilogElement };

            var loggerConfigElementCollection = new Mock<ILoggerConfigElementCollection>();
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
            var providerTypes = Common.Logging.Logger.GetLoggerTypes(config.Object);

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
    }
}