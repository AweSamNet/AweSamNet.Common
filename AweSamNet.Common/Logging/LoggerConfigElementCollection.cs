using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace AweSamNet.Common.Logging
{
    //http://stackoverflow.com/questions/1755421/c-sharp-appsettings-is-there-a-easy-way-to-put-a-collection-into-appsetting
    public class LoggerConfigElementCollection : ConfigurationElementCollection, ILoggerConfigElementCollection
    {
        public LoggerConfigElement this[int index]
        {
            get { return base.BaseGet(index) as LoggerConfigElement; }

        }

        public new virtual IEnumerator<LoggerConfigElement> GetEnumerator()
        {
            var collection = base.GetEnumerator();
            while (collection.MoveNext())
            {
                yield return collection.Current as LoggerConfigElement;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LoggerConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LoggerConfigElement)(element)).FullName;
        }
    }
}
