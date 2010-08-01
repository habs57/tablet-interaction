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
using System.Windows.Media.Effects;

namespace TablectionSketch.Controls
{
    /// <summary>
    /// Interaction logic for Liner.xaml
    /// </summary>
    public partial class Liner : UserControl
    {
        public TransformGroup transformGroup = new TransformGroup();
        public TranslateTransform traslation = new TranslateTransform(0, 0);
        public ScaleTransform scale = new ScaleTransform(1, 1);
        public RotateTransform rotate = new RotateTransform(0);

        public Liner()
        {
            InitializeComponent();
            
            this.Cursor = Cursors.SizeAll;
            this.ForceCursor = true;

            this.Effect = new DropShadowEffect() { ShadowDepth = 0, BlurRadius = 5 };

            this.IsManipulationEnabled = true;

            this.transformGroup.Children.Add(this.rotate);
            this.transformGroup.Children.Add(this.scale);
            this.transformGroup.Children.Add(this.traslation);

            this.RenderTransform = this.transformGroup;
        }

        private void DrawGradations()
        {
            int x = 10;

            while (x < this.Width)
            {
                Line line = new Line();
                line.Stroke = Brushes.Gray;
                line.StrokeThickness = 1.0;
                line.X1 = x;
                line.Y1 = 0;
                line.X2 = x;
                line.Y2 = 30;
                this.AddChild(line);
                x += 10;
            }
        }
    }
}
