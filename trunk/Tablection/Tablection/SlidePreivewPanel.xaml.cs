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
    /// Interaction logic for SlidePreivewPanel.xaml
    /// </summary>
    public partial class SlidePreivewPanel : UserControl
    {
        public SlidePreivewPanel()
        {
            InitializeComponent();


        }

        private void SlideListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SlideRepository.CurrentSlide = e.AddedItems[0] as Slide;                
            }            
        }
    }
}
