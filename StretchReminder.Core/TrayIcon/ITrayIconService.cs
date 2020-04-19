using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StretchReminder.Core.TrayIcon
{
    /// <summary>
    /// Service handles hiding/showing the application in the windows tray icons area.
    /// </summary>
    public interface ITrayIconService
    {
        ICommand StateChangedCommand { get; }
        ICommand ClosingCommand { get; }
        WindowState WindowState { get; }
    }
}
