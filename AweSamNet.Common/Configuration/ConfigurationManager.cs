using System.Collections.Specialized;
using System.Configuration;
using AweSamNet.Common.Logging;

namespace AweSamNet.Common.Configuration
{
    /// <summary>
    ///     Provides access to configuration files for client applications. This class
    ///     cannot be inherited.
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        /// <summary>
        ///     Gets the System.Configuration.AppSettingsSection data for the current application's
        ///     default configuration.
        /// </summary> 
        /// <returns>
        ///     Returns a System.Collections.Specialized.NameValueCollection object that
        ///     contains the contents of the System.Configuration.AppSettingsSection object
        ///     for the current application's default configuration.
        /// </returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">   
        ///     Could not retrieve a System.Collections.Specialized.NameValueCollection object
        ///     with the application settings data.
        /// </exception>
        public NameValueCollection AppSettings
        {
            get { return System.Configuration.ConfigurationManager.AppSettings; }
        }

        /// <summary>
        ///     Gets the System.Configuration.ConnectionStringsSection data for the current
        ///     application's default configuration.
        /// </summary>
        /// <returns>
        ///     Returns a System.Configuration.ConnectionStringSettingsCollection object
        ///     that contains the contents of the System.Configuration.ConnectionStringsSection
        ///     object for the current application's default configuration.
        /// </returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">   
        ///     Could not retrieve a System.Configuration.ConnectionStringSettingsCollection
        ///     object.
        /// </exception>
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings; }
        }

        /// <summary>
        ///     Retrieves a specified configuration section for the current application's
        ///     default configuration.
        /// </summary>
        /// <param name="sectionName">The configuration section path and name.</param>
        /// <returns>
        ///     The specified System.Configuration.ConfigurationSection object, or null if
        ///     the section does not exist.
        /// </returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        public object GetSection(string sectionName)
        {
            return System.Configuration.ConfigurationManager.GetSection(sectionName);
        }

        /// <summary>
        ///     Opens the configuration file for the current application as a System.Configuration.Configuration
        ///     object.
        /// </summary>
        /// <param name="userLevel">
        ///     The System.Configuration.ConfigurationUserLevel for which you are opening
        ///     the configuration.
        /// </param>
        /// <returns>A System.Configuration.Configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        public System.Configuration.Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            return System.Configuration.ConfigurationManager.OpenExeConfiguration(userLevel);
        }

        /// <summary>
        ///     Opens the specified client configuration file as a System.Configuration.Configuration
        ///     object.
        /// </summary>
        /// <param name="exePath">The path of the executable (exe) file.</param>
        /// <returns>A System.Configuration.Configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        public System.Configuration.Configuration OpenExeConfiguration(string exePath)
        {
            return System.Configuration.ConfigurationManager.OpenExeConfiguration(exePath);
        }

        /// <summary>
        ///     Opens the machine configuration file on the current computer as a System.Configuration.Configuration
        ///     object.
        /// </summary>
        /// <returns>A System.Configuration.Configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        public System.Configuration.Configuration OpenMachineConfiguration()
        {
            return System.Configuration.ConfigurationManager.OpenMachineConfiguration();
        }
            
        /// <summary>
        ///     Opens the specified client configuration file as a System.Configuration.Configuration
        ///     object that uses the specified file mapping and user level.
        /// </summary>
        /// <param name="fileMap">
        ///     An System.Configuration.ExeConfigurationFileMap object that references configuration
        ///     file to use instead of the application default configuration file.
        /// </param>
        /// <param name="userLevel">
        ///     The System.Configuration.ConfigurationUserLevel object for which you are
        ///     opening the configuration.
        /// </param>
        /// <returns>The configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        public System.Configuration.Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap,
            ConfigurationUserLevel userLevel)
        {
            return System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel);
        }

        /// <summary>
        ///     Opens the specified client configuration file as a System.Configuration.Configuration
        ///     object that uses the specified file mapping, user level, and preload option.
        /// </summary>
        /// <param name="fileMap">
        ///     An System.Configuration.ExeConfigurationFileMap object that references the
        ///     configuration file to use instead of the default application configuration
        ///     file.
        /// </param>
        /// <param name="userLevel">
        ///     The System.Configuration.ConfigurationUserLevel object for which you are
        ///     opening the configuration.
        /// </param>
        /// <param name="preLoad">true to preload all section groups and sections; otherwise, false.</param>
        /// <returns>The configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>

        public System.Configuration.Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap,
            ConfigurationUserLevel userLevel, bool preLoad)
        {
            return System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel, preLoad);
        }
        /// <summary>
        ///     Opens the machine configuration file as a System.Configuration.Configuration
        ///     object that uses the specified file mapping.
        /// </summary>
        /// <param name="fileMap">
        ///     An System.Configuration.ExeConfigurationFileMap object that references configuration
        ///     file to use instead of the application default configuration file.
        /// </param>
        /// <returns>A System.Configuration.Configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        public System.Configuration.Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
        {
            return System.Configuration.ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
        }

        /// <summary>
        ///     Refreshes the named section so the next time that it is retrieved it will
        ///     be re-read from disk.
        /// </summary>
        /// <param name="sectionName">
        ///     The configuration section name or the configuration path and section name
        ///     of the section to refresh.
        /// </param>
        public void RefreshSection(string sectionName)
        {
            System.Configuration.ConfigurationManager.RefreshSection(sectionName);
        }

        /// <summary>
        /// Writes settings to the config file and then refreshes it.
        /// </summary>
        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                Logger.Default.Error(ex, "key={key}, value={value} - Error writing app settings", key, value );
            }
        }

    }
}
