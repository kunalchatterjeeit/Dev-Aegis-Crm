using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Business.Common
{
    public static class Settings
    {
        private static readonly SettingsConfigurationSection _config = (SettingsConfigurationSection)ConfigurationManager.GetSection("cachingConfiguration");

        public static bool Enabled
        {
            get
            {
                return _config.Enabled;
            }
        }

        public static TimeSpan SlidingExpiration
        {
            get
            {
                return _config.SlidingExpiration;
            }
        }

        public static TimeSpan CacheExpiration
        {
            get
            {
                return _config.CacheExpiration;
            }
        }
    }
}
