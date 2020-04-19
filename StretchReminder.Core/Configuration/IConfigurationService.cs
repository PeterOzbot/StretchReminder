using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StretchReminder.Core.Configuration
{
    public interface IConfigurationService
    {
        bool ShowTrayIconBaloonTip { get; }
    }
}
