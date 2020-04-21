using System.Drawing;

namespace StretchReminder.TrayIcon.Core
{
    /// <summary>
    /// Defines the configurable data about the tray icon.
    /// </summary>
    public interface IIconInfo
    {
        string Title { get; }
        Icon Icon { get; }
        bool ShowBaloonTip { get; }
        string BalloonTipText { get; }
    }
}
