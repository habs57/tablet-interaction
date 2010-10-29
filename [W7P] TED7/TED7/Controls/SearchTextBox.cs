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

namespace TED7.Controls
{
    public sealed class SearchTextBox : TextBox
    {
        public static readonly DependencyProperty IsUpdateSourceProperty =
            DependencyProperty.Register(
            "IsUpdateSource", 
            typeof(Boolean),
            typeof(SearchTextBox), 
            new PropertyMetadata(false, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var txt = obj as TextBox;
            if (txt == null)
                return;
            if ((bool)e.NewValue)
            {
                txt.TextChanged += SearchTextBox_TextChanged;
            }
            else
            {
                txt.TextChanged -= SearchTextBox_TextChanged;
            }
        }

        public bool IsUpdateSource
        {
            get { return (bool)GetValue(IsUpdateSourceProperty); }
            set { SetValue(IsUpdateSourceProperty, value); }
        }          

        static void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {            
            SearchTextBox txt = sender as SearchTextBox;
            var expression = txt.GetBindingExpression(TextBox.TextProperty);
            if (expression != null)
            {
                expression.UpdateSource();
            }                        
        }
    }
}
