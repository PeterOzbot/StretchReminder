using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StretchReminder.Ui.Behaviors
{
    public static class DraggableBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(DraggableBehavior), new UIPropertyMetadata(false, OnValueChanged));

        public static bool GetIsEnabled(Control o) { return (bool)o.GetValue(IsEnabledProperty); }

        public static void SetIsEnabled(Control o, bool value) { o.SetValue(IsEnabledProperty, value); }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = dependencyObject as Window;
            if (uiElement == null) return;
            if (e.NewValue is bool && (bool)e.NewValue)
            {
                uiElement.MouseDown += UiElement_MouseDown;
            }
            else
            {
                uiElement.MouseDown -= UiElement_MouseDown;
            }
        }

        private static void UiElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                (sender as Window)?.DragMove();
        }
    }
}
