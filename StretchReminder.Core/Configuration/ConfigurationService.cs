using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StretchReminder.Core.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private bool? _showTrayIconBaloonTip;
        public bool ShowTrayIconBaloonTip
        {
            get
            {
                if (!_showTrayIconBaloonTip.HasValue)
                {
                    _showTrayIconBaloonTip = GetConfiguration<bool>(nameof(ShowTrayIconBaloonTip));
                    if (!_showTrayIconBaloonTip.HasValue)
                    {
                        return false;
                    }
                }
                return _showTrayIconBaloonTip.Value;
            }
        }

        private T GetConfiguration<T>(string configurationName)
        {
            try
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[configurationName], typeof(T));
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Cannot load setting: {configurationName}. Maybe the value is wrong or the setting is missing.", ex);
            }
        }
    }
}
