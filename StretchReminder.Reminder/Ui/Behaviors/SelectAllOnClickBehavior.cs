using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StretchReminder.Reminder.Ui.Behaviors
{
    public static class SelectAllOnClickBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(SelectAllOnClickBehavior), new UIPropertyMetadata(false, OnValueChanged));

        public static bool GetIsEnabled(Control o) { return (bool)o.GetValue(IsEnabledProperty); }

        public static void SetIsEnabled(Control o, bool value) { o.SetValue(IsEnabledProperty, value); }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = dependencyObject as TextBox;
            if (uiElement == null) return;
            if (e.NewValue is bool && (bool)e.NewValue)
            {
                uiElement.PreviewMouseLeftButtonDown += SelectivelyIgnoreMouseButton;
                uiElement.GotKeyboardFocus += SelectAllText;
                uiElement.MouseDoubleClick += SelectAllText;
            }

            else
            {
                uiElement.PreviewMouseLeftButtonDown -= SelectivelyIgnoreMouseButton;
                uiElement.GotKeyboardFocus -= SelectAllText;
                uiElement.MouseDoubleClick -= SelectAllText;
            }
        }

        private static void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            // Find the TextBox
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
                parent = VisualTreeHelper.GetParent(parent);

            if (parent != null)
            {
                var textBox = (TextBox)parent;
                if (!textBox.IsKeyboardFocusWithin)
                {
                    // If the text box is not yet focused, give it the focus and
                    // stop further processing of this click event.
                    textBox.Focus();
                    e.Handled = true;
                }
            }
        }

        private static void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }
    }
}
