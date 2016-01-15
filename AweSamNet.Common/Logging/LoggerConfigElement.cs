using System.Configuration;

namespace AweSamNet.Common.Logging
{
    //http://stackoverflow.com/questions/1755421/c-sharp-appsettings-is-there-a-easy-way-to-put-a-collection-into-appsetting
    public class LoggerConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("fullName", IsRequired = true)]
        public string FullName
        {
            get { return this["fullName"] as string; }
            set { this["fullName"] = value; }
        }

        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return this["assembly"] as string; }
            set { this["assembly"] = value; }
        }

        [ConfigurationProperty("enabled",IsRequired = false)]
        public bool Enabled
        {
            get
            {
                bool value;
                return !bool.TryParse(this["enabled"] as string, out value) || value;
            }
            set { this["enabled"] = value.ToString(); }
        }
    }
}
