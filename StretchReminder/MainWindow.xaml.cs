using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace StretchReminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            // initialise code here
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = "The app has been minimised. Click the tray icon to show.";
            m_notifyIcon.BalloonTipTitle = "The App";
            m_notifyIcon.Text = "The App";
            m_notifyIcon.Icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/StretchReminderIcon.ico")).Stream); 
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
        }


        TimeSpan _duration = new TimeSpan(0, 0, 10);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ShowNotification("balalbal", 10);
            DateTime startTime = DateTime.Now;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Do Stuff
            _duration = _duration.Subtract(new TimeSpan(0, 0, 0, 1));
            if (_duration.Seconds == 0)
            {

                return;
            }

            // Updating the Label which displays the current second
            Label.Content = _duration.ToString("mm':'ss");
        }

        public void ShowNotification(string description, double amount)
        {
            string xml = $@"<toast>
                      <visual>
                        <binding template='ToastGeneric'>
                          <text>Expense added</text>
                          <text>Description: {description} - Amount: {amount} </text>
                        </binding>
                      </visual>
                    </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /////////////////////////////////////////////////////////////
        void OnClose(object sender, CancelEventArgs args)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }

        private WindowState m_storedWindowState = WindowState.Normal;
        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (m_notifyIcon != null)
                    m_notifyIcon.ShowBalloonTip(2000);
            }
            else
                m_storedWindowState = WindowState;
        }
        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }


    }
}
