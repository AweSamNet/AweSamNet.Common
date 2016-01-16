using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace AweSamNet.Common.Logging
{
    public interface ILoggerConfigElementCollection
    {
        LoggerConfigElement this[int index] { get; }
        int Count { get; }
        bool EmitClear { get; set; }
        bool IsSynchronized { get; }
        object SyncRoot { get; }
        ConfigurationElementCollectionType CollectionType { get; }
        ConfigurationLockCollection LockAttributes { get; }
        ConfigurationLockCollection LockAllAttributesExcept { get; }
        ConfigurationLockCollection LockElements { get; }
        ConfigurationLockCollection LockAllElementsExcept { get; }
        bool LockItem { get; set; }
        ElementInformation ElementInformation { get; }
        System.Configuration.Configuration CurrentConfiguration { get; }
        IEnumerator<LoggerConfigElement> GetEnumerator();
        bool IsReadOnly();
        bool Equals(object compareTo);
        int GetHashCode();
        void CopyTo(ConfigurationElement[] array, int index);
    }
}