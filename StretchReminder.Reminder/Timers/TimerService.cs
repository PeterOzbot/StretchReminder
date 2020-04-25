using StretchReminder.Reminder.Notifications;
using System;

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
            // set new duration
            _timerDuration = new TimerDuration { Duration = duration, Readonly = true };

            // start the timer
            Start(_timerDuration);

            // return the duration
            return _timerDuration;
        }

        private void Start(TimerDuration timerDuration)
        {
            if (_dispatcherTimer == null)
            {
                // create new dispatcher and start it
                _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                _dispatcherTimer.Tick += DispatcherTimer_Tick;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                _dispatcherTimer.Start();
            }
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

                // show notification
                _notificationService.Show(new DefaultNotification
                {
                    Message = "Stretch lazy boy.",
                    OnSnooze = Snooze
                });
            }
        }

        private void Snooze(TimeSpan duration)
        {
            if (_timerDuration != null)
            {
                // set the new duration
                _timerDuration.Duration = duration;

                // start the timer with new duration
                Start(_timerDuration);
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

            // unlock the timer duration
            if (_timerDuration != null)
            {
                _timerDuration.Readonly = false;
            }
        }
    }
}
