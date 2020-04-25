using System;

namespace StretchReminder.Reminder.Notifications
{
    public class DefaultNotification: INotification
    {
        public string Message { get; set; }

        public Action<TimeSpan> OnSnooze { get; set; }
    }
}
