using StretchReminder.Reminder.Notifications;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StretchReminder.Reminder.Timers
{
    public class TimerService : ITimerService
    {
        // instance with duration that is shared with users of the timer service
        private TimerDuration _timerDuration;
        // actual timer that measures timer
        private System.Windows.Threading.DispatcherTimer _dispatcherTimer;
        // used to notify the user when the timer runs out
        private INotificationService _notificationService;

        public TimerService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        public TimerDuration Start(TimeSpan duration)
        {
            if (_dispatcherTimer == null)
            {
                // set new duration
                _timerDuration = new TimerDuration { Duration = duration, Readonly = true };

                // create new dispatcher and start it
                _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                _dispatcherTimer.Tick += DispatcherTimer_Tick;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                _dispatcherTimer.Start();
            }

            return _timerDuration;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // reduce the duration each tick
            _timerDuration.Duration = _timerDuration.Duration.Subtract(new TimeSpan(0, 0, 0, 1));

            // check if duration run out
            if (_timerDuration.Duration.Seconds == 0)
            {
                // stop the timer
                Stop();

                // unlock the timer duration
                _timerDuration.Readonly = false;

                // show notification
                _notificationService.Show(new Notification
                {
                    Message = "Stretch lazy boy."
                });
            }
        }

        public void Stop()
        {
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
                _dispatcherTimer.Tick -= DispatcherTimer_Tick;
                _dispatcherTimer = null;
            }
        }
    }
}
