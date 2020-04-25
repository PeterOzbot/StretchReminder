using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace StretchReminder.Reminder.Notifications
{
    /// <summary>
    /// Concrete implementation of <see cref="IReminderService"/>
    /// </summary>
    public class NotificationService : INotificationService
    {
        public void Show(INotification notification)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml($@"
<toast duration='long' scenario='alarm'>
    <visual>
        <binding template='ToastGeneric' attribution='by Stretch Reminder'>
            <text>{notification.Message}</text>
        </binding>
    </visual>
    <audio src='ms-winsoundevent:Notification.Looping.Alarm' loop='true'>
    </audio>
    <actions>
        <action
            content='Snooze'
            imageUri = 'Images/snooze.png'
            arguments = '{NotificationAction.Snooze15}'
            activationType = 'Foreground' />
    </actions>
</toast>
");

            ToastNotification toast = new ToastNotification(doc);
            toast.Activated += (sender, args) => Activate(notification, args);
            var toastNotifier = ToastNotificationManager.CreateToastNotifier();
            toastNotifier.Show(toast);
        }

        /// <summary>
        /// Activates the notification with the parsed arguments.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="args"></param>
        private void Activate(INotification notification, object args)
        {
            var arguments = args as Windows.UI.Notifications.ToastActivatedEventArgs;
            if (notification != null && arguments != null)
            {
                NotificationAction notificationAction;
                if (Enum.TryParse<NotificationAction>(arguments.Arguments?.ToString(), out notificationAction))
                {
                    NotifyAction(notification, notificationAction);
                }
            }
        }

        /// <summary>
        /// Handles specific notification action.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="notificationAction"></param>
        private void NotifyAction(INotification notification, NotificationAction notificationAction)
        {
            switch (notificationAction)
            {
                case NotificationAction.Snooze15:
                    notification.OnSnooze?.Invoke(new TimeSpan(0, 0, 2));
                    break;
                default:
                    throw new NotSupportedException($"{notificationAction} is not supported.");
            }
        }
    }
}
