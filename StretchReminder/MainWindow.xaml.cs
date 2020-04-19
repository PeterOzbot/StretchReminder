using CommonServiceLocator;
using StretchReminder.Core.Events;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StretchReminder
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
            eventAggregator.Register<HideEvent>(()=> Hide());
            eventAggregator.Register<ShowEvent>(() => Show());
        }
    }
}
