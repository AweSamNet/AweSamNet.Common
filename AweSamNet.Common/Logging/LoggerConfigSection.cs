using System.Configuration;

namespace AweSamNet.Common.Logging
{
    //http://stackoverflow.com/questions/1755421/c-sharp-appsettings-is-there-a-easy-way-to-put-a-collection-into-appsetting
    public class LoggerConfigSection : ConfigurationSection, ILoggerConfigSection
    {
        [ConfigurationProperty("loggerCollection")]
        public LoggerConfigElementCollection AllValues
        {
            get { return this["loggerCollection"] as LoggerConfigElementCollection; }
        }

        public static LoggerConfigSection GetConfigSection()
        {
            return ConfigurationManager.GetSection("LoggerSection") as LoggerConfigSection;
        }
    }
}