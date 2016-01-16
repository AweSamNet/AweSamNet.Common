using System.Collections.Specialized;
using System.Configuration;

namespace AweSamNet.Common.Configuration
{
    public interface IConfigurationManager
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
        NameValueCollection AppSettings { get; }

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
        ConnectionStringSettingsCollection ConnectionStrings { get; }

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
        object GetSection(string sectionName);

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
        System.Configuration.Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);

        /// <summary>
        ///     Opens the specified client configuration file as a System.Configuration.Configuration
        ///     object.
        /// </summary>
        /// <param name="exePath">The path of the executable (exe) file.</param>
        /// <returns>A System.Configuration.Configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        System.Configuration.Configuration OpenExeConfiguration(string exePath);

        /// <summary>
        ///     Opens the machine configuration file on the current computer as a System.Configuration.Configuration
        ///     object.
        /// </summary>
        /// <returns>A System.Configuration.Configuration object.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        System.Configuration.Configuration OpenMachineConfiguration();

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
        System.Configuration.Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap,
            ConfigurationUserLevel userLevel);

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
        System.Configuration.Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap,
            ConfigurationUserLevel userLevel, bool preLoad);

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
        System.Configuration.Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);

        /// <summary>
        ///     Refreshes the named section so the next time that it is retrieved it will
        ///     be re-read from disk.
        /// </summary>
        /// <param name="sectionName">
        ///     The configuration section name or the configuration path and section name
        ///     of the section to refresh.
        /// </param>
        void RefreshSection(string sectionName);
    }
}