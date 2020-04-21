namespace StretchReminder.TrayIcon.Core
{
    /// <summary>
    /// Service handles hiding/showing the application in the windows tray icons area.
    /// </summary>
    public interface ITrayIconService
    {
        /// <summary>
        /// Constructs configurable part of tray icon.
        /// </summary>
        /// <returns></returns>
        IIconInfo GetIconInfo();
        /// <summary>
        /// Hides the application to the minimized state in the tray icon area.
        /// </summary>
        void HideApp();
        /// <summary>
        /// Shows the application from the minimized state when in tray icon area.
        /// </summary>
        void ShowApp();
    }
}
