using System;
using System.ComponentModel;
using System.Configuration;

namespace Business.Common
{
    public class SettingsConfigurationSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty _Enabled;
        private static readonly ConfigurationProperty _CacheExpiration;
        private static readonly ConfigurationProperty _SlidingExpiration;
        private static readonly ConfigurationPropertyCollection _Properties;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static SettingsConfigurationSection()
        {
            _Enabled = new ConfigurationProperty("enabled", typeof(bool), false);
            _CacheExpiration = new ConfigurationProperty("cacheExpiration", typeof(TimeSpan), new TimeSpan(0, 00, 0), ConfigurationPropertyOptions.IsRequired);
            _SlidingExpiration = new ConfigurationProperty("slidingExpiration", typeof(TimeSpan), new TimeSpan(0, 00, 0));
            _Properties = new ConfigurationPropertyCollection() {
                _Enabled,
                _CacheExpiration,
                _SlidingExpiration
			};

        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }

        [ConfigurationProperty("enabled", DefaultValue = false)]
        public bool Enabled
        {
            get
            {
                return (bool)base[_Enabled];
            }
        }

        [ConfigurationProperty("cacheExpiration", DefaultValue = "00:00:00"),
        TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807"),
        TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan CacheExpiration
        {
            get
            {
                return (TimeSpan)base[_CacheExpiration];
            }
        }

        [ConfigurationProperty("slidingExpiration", DefaultValue = "00:00:00"),
        TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807"),
        TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan SlidingExpiration
        {
            get
            {
                return (TimeSpan)base[_SlidingExpiration];
            }
        }
    }
}
