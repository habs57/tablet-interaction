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
using System.Windows.Shapes;
using System.Diagnostics;

namespace Tablection.Desktop
{
    /// <summary>
    /// Interaction logic for IconInfoBrowser.xaml
    /// </summary>
    public partial class IconInfoBrowser : Window
    {
        public IconInfoBrowser()
        {
            InitializeComponent();
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            e.Handled = true;            
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            Matrix transformMatrix = ((MatrixTransform)this.LayoutTransform).Matrix;

            transformMatrix.ScaleAt(e.DeltaManipulation.Scale.X,
                                 e.DeltaManipulation.Scale.X,
                                 e.ManipulationOrigin.X,
                                 e.ManipulationOrigin.Y);

            this.LayoutTransform = new MatrixTransform(transformMatrix);
            //this.RenderTransform = new MatrixTransform(transformMatrix);

            e.Handled = true;            
        }
    }
}
