using GalaSoft.MvvmLight;
using StretchReminder.Reminder.Timers;
using StretchReminder.Reminder.Ui.ViewModels;
using StretchReminder.TrayIcon.Implementation;

namespace StretchReminder.Ui.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        // used to handle logic for hiding the application in the tray area
        public TrayIconViewModel TrayIconViewModel { get; }
        // used to handle logic for timer and the notifications
        public ReminderViewModel ReminderViewModel { get; }
        // used for running the timer
        public ITimerService TimerService { get; }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ITimerService timerService, TrayIconViewModel trayIconViewModel, ReminderViewModel reminderViewModel)
        {
            TimerService = timerService;
            TrayIconViewModel = trayIconViewModel;
            ReminderViewModel = reminderViewModel;
        }
    }
}