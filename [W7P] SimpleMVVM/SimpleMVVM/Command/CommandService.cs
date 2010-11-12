using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;

namespace SimpleMVVM
{
    /// <summary>
    /// Command Service 
    /// for Silverlight in Windows 7 phone
    /// http://simonguest.com/2010/10/18/test-driven-development-tdd-and-windows-phone-7/
    /// </summary>
    public static class CommandService
    {
        private static readonly DependencyProperty _eventProperty;
        private static readonly DependencyProperty _commandProperty;
        private static readonly DependencyProperty _useParameterProperty;

        static CommandService()
        {
            _eventProperty = DependencyProperty.RegisterAttached("Event", typeof(string), typeof(CommandService),
            new PropertyMetadata("Click"));

            _commandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandService),
            new PropertyMetadata(OnCommandChanged));

            _useParameterProperty = DependencyProperty.RegisterAttached("UseParameter", typeof(bool), typeof(CommandService),
           new PropertyMetadata(false));
        }

        public static string GetEvent(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(_eventProperty);
        }

        public static void SetEvent(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(_eventProperty, value);
        }

        public static ICommand GetCommand(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(_commandProperty);
        }

        public static void SetCommand(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(_commandProperty, value);
        }

        public static bool GetUseParameter(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(_useParameterProperty);
        }

        public static void SetUseParameter(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(_useParameterProperty, value);
        }

        private static void OnCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dpceArgs)
        {
            string eventName = GetEvent(dependencyObject);
            var clickEvent = dependencyObject.GetType().GetEvents().FirstOrDefault(eventInfo => eventInfo.Name.Equals(eventName));
            if (clickEvent != null)
            {
                string parameter = dependencyObject.GetValue(_commandProperty).ToString();
                ICommand command = (ICommand)dpceArgs.NewValue;

                bool useParameter = GetUseParameter(dependencyObject);

                if (clickEvent.EventHandlerType.Equals(typeof(RoutedEventHandler)) == true)
                {
                    RoutedEventHandler handler = null;
                    
                    if (useParameter == false)
                    {
                        handler = delegate(object sender, RoutedEventArgs arg) { command.Execute(parameter); };
                    }
                    else
                    {
                        handler = delegate(object sender, RoutedEventArgs arg) { command.Execute(arg); };
                    }

                    clickEvent.AddEventHandler((object)dependencyObject, handler);                    
                }
                else if (clickEvent.EventHandlerType.Equals(typeof(RoutedPropertyChangedEventHandler<double>)) == true)
                {
                    RoutedPropertyChangedEventHandler<double> handler = null;
                                        
                    if (useParameter == false)
                    {
                        handler = delegate(object sender, RoutedPropertyChangedEventArgs<double> arg) { command.Execute(parameter); };
                    }
                    else
                    {
                        handler = delegate(object sender, RoutedPropertyChangedEventArgs<double> arg) { command.Execute(arg); };
                    }

                    clickEvent.AddEventHandler((object)dependencyObject, handler);                    
                }                
            }
        }
    }
}
