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
    /// <summary>
    /// Interaction logic for SlideMainPanel.xaml
    /// </summary>
    public partial class SlideMainPanel : UserControl
    {
        public SlideMainPanel()
        {
            InitializeComponent();

            InkCanvasSettings.DrawingAttributeChanged += new Action<System.Windows.Ink.DrawingAttributes>(InkCanvasSettings_DrawingAttributeChanged);
            InkCanvasSettings.EditingModeChanged += new Action<InkCanvasEditingMode>(InkCanvasSettings_EditingModeChanged);
            SlideRepository.SlideSelectionChanged += new Action<Slide>(SlideRepository_SlideSelectionChanged);
        }

        void InkCanvasSettings_EditingModeChanged(InkCanvasEditingMode obj)
        {
            inkCanvas.EditingMode = obj;
        }

        void InkCanvasSettings_DrawingAttributeChanged(System.Windows.Ink.DrawingAttributes obj)
        {
            inkCanvas.DefaultDrawingAttributes = obj;
        }
        
        public static ImageBrush _inkCanvasBrush = new ImageBrush();

        void SlideRepository_SlideSelectionChanged(Slide obj)
        {
            _inkCanvasBrush.ImageSource = obj.Image;
            inkCanvas.Background = _inkCanvasBrush;
        }
                
    }
}
