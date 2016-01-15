using System.Configuration;

namespace AweSamNet.Common.Logging
{
    //http://stackoverflow.com/questions/1755421/c-sharp-appsettings-is-there-a-easy-way-to-put-a-collection-into-appsetting
    public class LoggerConfigSection : ConfigurationSection, ILoggerConfigSection
    {
        [ConfigurationProperty("loggerCollection")]
        public ILoggerConfigElementCollection AllValues
        {
            get { return this["loggerCollection"] as LoggerConfigElementCollection; }
        }
    }
}