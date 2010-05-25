using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TablectionSketch
{
    public class SlideListBox : ListBox
    {
        static SlideListBox()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(SlideListBox), new FrameworkPropertyMetadata(typeof(SlideListBox)));            
        }

        public SlideListBox()
        {
            this.SetValue(KeyboardNavigation.DirectionalNavigationProperty, KeyboardNavigationMode.Cycle);
            this.SetValue(ScrollViewer.PanningDecelerationProperty, 0.5);
            this.SetValue(ScrollViewer.PanningModeProperty, PanningMode.HorizontalOnly);
            this.SetValue(ScrollViewer.CanContentScrollProperty, false);
            //this.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(ScrollChangedHandler));
        }

        void ScrollChangedHandler(object sender, ScrollChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0}, {1}, {2}, {3}", this.ActualHeight, args.ExtentHeight, args.ViewportHeight, args.VerticalOffset));
        }
    }
}
