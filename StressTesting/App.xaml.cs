using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StressTesting
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.PreviewMouseLeftButtonDownEvent,
                new MouseButtonEventHandler(SelectivelyIgnoreMouseButton));
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.GotKeyboardFocusEvent,
                new RoutedEventHandler(SelectAllText));
            EventManager.RegisterClassHandler(typeof(TextBox), Control.MouseDoubleClickEvent,
                new RoutedEventHandler(SelectAllText));
            base.OnStartup(e);
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
                parent = VisualTreeHelper.GetParent(parent);

            if (parent != null)
            {
                var textBox = (TextBox)parent;
                if (!textBox.IsKeyboardFocusWithin)
                {
                    textBox.Focus();
                    e.Handled = true;
                }
            }
        }

        private void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            textBox?.SelectAll();
        }
    }
}
