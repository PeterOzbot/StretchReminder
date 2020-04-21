using System;
using System.Collections.Generic;
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
        public void Show(Notification reminder)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml($@"<toast>
                      <visual>
                        <binding template='ToastGeneric'>
                          <text>{reminder.Message}</text>
                        </binding>
                      </visual>
                    </toast>");

            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
