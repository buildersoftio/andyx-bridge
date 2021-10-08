using System;
using System.IO;

namespace Andy.X.Bridge.IO.Locations
{
    public static class AppLocations
    {
        #region Directories
        public static string GetRootDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string ConfigDirectory()
        {
            return Path.Combine(GetRootDirectory(), "config");
        }

        public static string ServicesDirectory()
        {
            return Path.Combine(GetRootDirectory(), "services");
        }

        public static string TemplatesDirectory()
        {
            return Path.Combine(GetRootDirectory(), "templates");
        }

        public static string LogsDirectory()
        {
            return Path.Combine(GetRootDirectory(), "logs");
        }
        #endregion

        #region Configuration Files
        public static string GetAndyXConfigurationFile()
        {
            return Path.Combine(ConfigDirectory(), "andyx_config.json");
        }

        public static string GetQueueConfigurationFile()
        {
            return Path.Combine(ConfigDirectory(), "queues_config.json");
        }

        public static string GetLogConfigurationFile()
        {
            return Path.Combine(LogsDirectory(), $"xbridge-{DateTime.Now:dd-MM-YYYY}.log");
        }

        #endregion
    }
}
