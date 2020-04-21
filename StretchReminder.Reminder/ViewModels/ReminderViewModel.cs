using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StretchReminder.Reminder.Notifications;
using StretchReminder.Reminder.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StretchReminder.Reminder.ViewModels
{
    public class ReminderViewModel : ViewModelBase
    {
        private RelayCommand _startCommand;
        /// <summary>
        /// Command which starts the timer running.
        /// </summary>
        public ICommand StartCommand => _startCommand;

        private RelayCommand _stopCommand;
        /// <summary>
        /// Command which stops the timer running.
        /// </summary>
        public ICommand StopCommand => _stopCommand;

        private TimerDuration _timerDuration = new TimerDuration();
        /// <summary>
        /// The duration that passes before the notification is shown to the user.
        /// </summary>
        public TimerDuration TimerDuration
        {
            get { return _timerDuration; }
            set
            {
                _timerDuration = value;
                RaisePropertyChanged();
            }
        }

        // service which handles the timer
        private ITimerService _timerService;

        public ReminderViewModel(ITimerService timerService)
        {
            _timerService = timerService;

            // initialize commands
            _startCommand = new RelayCommand(Start);
            _stopCommand = new RelayCommand(Stop);
        }

        private void Start()
        {
            if (CanStart())
            {
                TimerDuration = _timerService.Start(new TimeSpan(TimerDuration.Hours, TimerDuration.Minutes, TimerDuration.Seconds));
            }
        }

        private bool CanStart()
        {
            return TimerDuration.Hours != 0 || TimerDuration.Minutes != 0 || TimerDuration.Seconds != 0;
        }

        private void Stop()
        {
            _timerService.Stop();
        }
    }
}
