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

        private void Grid_TouchDown(object sender, TouchEventArgs e)
        {
            ToolPanel.Visibility = Visibility.Visible;            
        }
                
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToolPanel.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.SlideList.Visibility = System.Windows.Visibility.Visible;
        }

        private void SlideListHideAnimation_Completed(object sender, EventArgs e)
        {
            this.SlideList.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ToolPanelHideAnimaton_Completed(object sender, EventArgs e)
        {
            ToolPanel.Visibility = Visibility.Collapsed;
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
        
    }
}
