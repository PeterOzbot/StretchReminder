using StretchReminder.TrayIcon.Core;
using System.Drawing;

namespace StretchReminder.TrayIcon.Implementation
{
    public class IconInfo : IIconInfo
    {
        public string Title { get; set; }
        public Icon Icon { get; set; }
        public bool ShowBaloonTip { get; set; }
        public string BalloonTipText { get; set; }
    }
}
