namespace StretchReminder.Reminder.Notifications
{
    /// <summary>
    /// Defines service which shows the reminder to the user.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Shows the notification.
        /// </summary>
        /// <param name="reminder"></param>
        void Show(Notification notification);
    }
}
