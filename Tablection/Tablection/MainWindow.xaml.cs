﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBottom_Click(object sender, RoutedEventArgs e)
        {        
            this.SlideList.Visibility = Visibility.Visible;
        }

        private void btnTop_Click(object sender, RoutedEventArgs e)
        {         
            this.ToolPanel.Visibility = Visibility.Visible;
        }
        
        private void SlideListHideAnimation_Completed(object sender, EventArgs e)
        {
            this.SlideList.Visibility = Visibility.Collapsed;
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
        
        private void SlideList_TouchEnter(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_TouchEnter");
        }

        private void SlideList_TouchLeave(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_TouchLeave");
        }
    
        private void SlideList_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_MouseEnter");
        }

        private void SlideList_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_MouseLeave");
        }

        private void ToolPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_MouseEnter");
        }

        private void ToolPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_MouseLeave");
        }

        private void ToolPanel_TouchEnter(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_TouchEnter");
        }

        private void ToolPanel_TouchLeave(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_TouchLeave");
        }


    }
}
