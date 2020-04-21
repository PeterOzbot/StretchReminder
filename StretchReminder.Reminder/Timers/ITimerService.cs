using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StretchReminder.Reminder.Timers
{
    /// <summary>
    /// Defines the service that handles the timer.
    /// </summary>
    public interface ITimerService
    {
        TimerDuration Start(TimeSpan duration);
        void Stop();
    }
}
