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

using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace TablectionSketch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _toolShowTimer = new DispatcherTimer();
        DispatcherTimer _slideShowTimer = new DispatcherTimer();


        Storyboard _toolHideAnimation;
        Storyboard _slideHideAnimation;
        
       
        public MainWindow()
        {
            InitializeComponent();


            //this._slideHideAnimation = this.FindResource("slideListHideAnimaton") as Storyboard;
            //this._toolHideAnimation = this.FindResource("toolPanelHideAnimaton") as Storyboard;

            //_slideShowTimer.Interval = TimeSpan.FromSeconds(3);
            //_slideShowTimer.Tick += new EventHandler(_slideShowTimer_Tick);

            //_toolShowTimer.Interval = TimeSpan.FromSeconds(3);
            //_toolShowTimer.Tick += new EventHandler(_toolShowTimer_Tick);
        }

        //void _slideShowTimer_Tick(object sender, EventArgs e)
        //{
        //    this._slideShowTimer.Stop();
        //    this._slideHideAnimation.Begin();
            
        //}

        //void _toolShowTimer_Tick(object sender, EventArgs e)
        //{
        //    this._toolHideAnimation.Stop();
        //    this._toolHideAnimation.Begin();
        //}

        private void btnBottom_Click(object sender, RoutedEventArgs e)
        {
            //this.RestartSlideTimer();
            this.SlideList.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnTop_Click(object sender, RoutedEventArgs e)
        {
            //this.RestartToolTimer();
            this.ToolPanel.Visibility = Visibility.Visible;
        }
        
        private void SlideListHideAnimation_Completed(object sender, EventArgs e)
        {
            this.SlideList.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ToolPanelHideAnimaton_Completed(object sender, EventArgs e)
        {
            this.ToolPanel.Visibility = Visibility.Collapsed;
        }
        

        private void HeaderBasicTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioTools.IsChecked = true;
        }

        private void HeaderColorTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioColors.IsChecked = true;
        }

        private void HeaderStrokeTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioStrokes.IsChecked = true;
        }

        private void DrawingCanvas_TouchUp(object sender, TouchEventArgs e)
        {
            
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        //private void RestartSlideTimer()
        //{
        //    if (this._slideShowTimer.IsEnabled == false)
        //    {
        //        this._slideShowTimer.Start();
        //    }
        //}

        //private void RestartToolTimer()
        //{
        //    if (this._toolShowTimer.IsEnabled == false)
        //    {
        //        this._toolShowTimer.Start();
        //    }
        //}

        private void SlideList_TouchEnter(object sender, TouchEventArgs e)
        {
            
        }

        private void SlideList_TouchLeave(object sender, TouchEventArgs e)
        {

        }

    }
}
