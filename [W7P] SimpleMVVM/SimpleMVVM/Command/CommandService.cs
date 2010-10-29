using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SimpleMVVM
{
    /// <summary>
    /// Command Service 
    /// for Silverlight in Windows 7 phone
    /// http://simonguest.com/2010/10/18/test-driven-development-tdd-and-windows-phone-7/
    /// </summary>
    public static class CommandService
    {
        private static readonly DependencyProperty _commandProperty;

        static CommandService()
        {
            _commandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandService),
            new PropertyMetadata(OnCommandChanged));
        }

        public static ICommand GetCommand(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(_commandProperty);
        }

        public static void SetCommand(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(_commandProperty, value);
        }

        private static void OnCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dpceArgs)
        {
            if (dependencyObject is Button)
            {
                string parameter = dependencyObject.GetValue(_commandProperty).ToString();
                Button button = (Button)dependencyObject;
                ICommand command = (ICommand)dpceArgs.NewValue;
                button.Click += delegate(object sender, RoutedEventArgs arg) { command.Execute(parameter); };
            }
        }
    }
}
