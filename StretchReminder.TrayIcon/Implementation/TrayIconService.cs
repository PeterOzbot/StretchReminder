using GalaSoft.MvvmLight.Command;
using StretchReminder.Core.Configuration;
using StretchReminder.Core.Events;
using StretchReminder.TrayIcon.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace StretchReminder.TrayIcon.Implementation
{
    /// <summary>
    /// Concrete implementation of <see cref="ITrayIconService"/>
    /// </summary>
    public class TrayIconService : ITrayIconService
    {
        // event aggregator for notifying when to hide/show the application
        private IEventAggregator _eventAggregator;
        // used to read the setting: ShowTrayIconBaloonTip
        private IConfigurationService _configurationService;

        /// <summary>
        /// Initializes the service.
        /// </summary>
        public TrayIconService(IEventAggregator eventAggregator, IConfigurationService configurationService)
        {
            _eventAggregator = eventAggregator;
            _configurationService = configurationService;
        }

        public IIconInfo GetIconInfo()
        {
            return new IconInfo
            {
                Title = "Stretch Reminder",
                Icon = new Icon(System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/StretchReminderIcon.ico")).Stream),
                BalloonTipText = "The application has been minimized. Click the tray icon to show.",
                ShowBaloonTip = _configurationService.ShowTrayIconBaloonTip,
            };
        }

        public void HideApp()
        {
            _eventAggregator.Raise<HideEvent>();
        }

        public void ShowApp()
        {
            _eventAggregator.Raise<ShowEvent>();
        }
    }
}
