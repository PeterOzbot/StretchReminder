using GalaSoft.MvvmLight.Command;
using StretchReminder.Core.Configuration;
using StretchReminder.Core.Events;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace StretchReminder.Core.TrayIcon
{
    /// <summary>
    /// Concrete implementation of <see cref="ITrayIconService"/>
    /// </summary>
    public class TrayIconService : ITrayIconService, INotifyPropertyChanged
    {
        // event aggregator for notifying when to hide/show the application
        private IEventAggregator _eventAggregator;
        // used to read the setting: ShowTrayIconBaloonTip
        private IConfigurationService _configurationService;


        private RelayCommand _stateChangedCommand;
        public ICommand StateChangedCommand => _stateChangedCommand;

        private RelayCommand _closingCommand;
        public ICommand ClosingCommand => _closingCommand;

        private WindowState _windowState;
        public WindowState WindowState { get { return _windowState; } set { _windowState = value; OnPropertyChanged(); } }


        // icon show in the tray area
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        // last state of the application
        private WindowState _storedWindowState = WindowState.Normal;


        /// <summary>
        /// Initializes the service.
        /// </summary>
        public TrayIconService(IEventAggregator eventAggregator, IConfigurationService configurationService)
        {
            _eventAggregator = eventAggregator;
            _configurationService = configurationService;

            // initialize commands
            _stateChangedCommand = new RelayCommand(OnStateChanged);
            _closingCommand = new RelayCommand(OnClose);

            InitializeIcon();
        }

        private void InitializeIcon()
        {
            // initialize code here
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Text = "Stretch Reminder";
            _notifyIcon.Icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/StretchReminderIcon.ico")).Stream);
            _notifyIcon.Click += new EventHandler(NotifyIcon_Click);

            if (_configurationService.ShowTrayIconBaloonTip)
            {
                _notifyIcon.BalloonTipText = "The application has been minimized. Click the tray icon to show.";
                _notifyIcon.BalloonTipTitle = "Stretch Reminder";
            }
        }

        private void OnStateChanged()
        {
            if (WindowState == WindowState.Minimized)
            {
                // notify to hide the application
                _eventAggregator.Raise<HideEvent>();

                // show the tray icon
                ShowTrayIcon(true);

                // notify user that the application is in the tray
                if (_configurationService.ShowTrayIconBaloonTip)
                {
                    _notifyIcon?.ShowBalloonTip(2000);
                }
            }
            else
                _storedWindowState = WindowState;
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            // hide the tray icon
            ShowTrayIcon(false);

            // restore previous state and notify to show the application
            WindowState = _storedWindowState;
            _eventAggregator.Raise<ShowEvent>();
        }

        private void ShowTrayIcon(bool show)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = show;
            }
        }

        private void OnClose()
        {
            _notifyIcon?.Dispose();
            _notifyIcon = null;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
