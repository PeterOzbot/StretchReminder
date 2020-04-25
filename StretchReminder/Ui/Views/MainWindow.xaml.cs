using CommonServiceLocator;
using StretchReminder.Core.Events;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StretchReminder.Ui.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // listen for the hide/show event
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.Register<HideEvent>(() => Hide());
            eventAggregator.Register<ShowEvent>(() => Show());
        }

        #region Close/Minimize
        private void MinimizeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
        #endregion
    }
}
