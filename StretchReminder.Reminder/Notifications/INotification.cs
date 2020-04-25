using System;

namespace StretchReminder.Reminder.Notifications
{
    /// <summary>
    /// Defines the notification that should be shown to the user.
    /// </summary>
    public interface INotification
    {
        string Message { get; }

        Action<TimeSpan> OnSnooze { get; }
    }

    /// <summary>
    /// Action that is done by the user with the notification.
    /// </summary>
    public enum NotificationAction
    {
        Snooze15
    }
}
