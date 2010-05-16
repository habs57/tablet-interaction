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

using System.Windows.Ink;

namespace TablectionSketch
{
    /// <summary>
    /// Interaction logic for ToolPanel.xaml
    /// </summary>
    public partial class ToolPanel : UserControl
    {
        public ToolPanel()
        {
            InitializeComponent();
        }

        private void lbEditingMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InkCanvasSettings.EdtingMode = (InkCanvasEditingMode)e.AddedItems[0];
        }

        private void lbDrawingAttributes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InkCanvasSettings.DrawingAttributes = (DrawingAttributes)e.AddedItems[0];
        }
    }
}
