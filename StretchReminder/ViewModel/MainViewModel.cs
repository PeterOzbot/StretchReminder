using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StretchReminder.Core.Reminders;
using StretchReminder.Core.TrayIcon;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace StretchReminder.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        // used to display the reminder to the user
        private IReminderService _reminderService;
        // used to handle logic for hiding the application in the tray area
        private ITrayIconService _trayIconService;
        public ITrayIconService TrayIconService { get { return _trayIconService; } }

        private RelayCommand _startTimeCommand;
        public ICommand StartTimeCommand => _startTimeCommand;

        private string _durationFriendlyName = "00:00";
        public string DurationFriendlyName
        {
            get { return _durationFriendlyName; }
            set
            {
                _durationFriendlyName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ITrayIconService trayIconService, IReminderService reminderService)
        {
            _trayIconService = trayIconService;
            _reminderService = reminderService;

            // initialize commands
            _startTimeCommand = new RelayCommand(OnStartTimer);
        }


        TimeSpan _duration = new TimeSpan(0, 0, 10);
        System.Windows.Threading.DispatcherTimer _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void OnStartTimer()
        {
            DateTime startTime = DateTime.Now;
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Do Stuff
            _duration = _duration.Subtract(new TimeSpan(0, 0, 0, 1));

            // Updating the Label which displays the current second
            DurationFriendlyName = _duration.ToString("mm':'ss");

            if (_duration.Seconds == 0)
            {
                _dispatcherTimer.Stop();
                _duration = new TimeSpan(0, 0, 10);
                _reminderService.ShowReminder(new Reminder { Message = "Stretch lazy boy." });
                return;
            }
        }
    }
}