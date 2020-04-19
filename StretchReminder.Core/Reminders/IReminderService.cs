using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StretchReminder.Core.Reminders
{
    /// <summary>
    /// Defines service which shows the reminder to the user.
    /// </summary>
    public interface IReminderService
    {
        /// <summary>
        /// Shows the reminder.
        /// </summary>
        /// <param name="reminder"></param>
        void ShowReminder(Reminder reminder);
    }
}
