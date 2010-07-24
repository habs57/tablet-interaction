using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media.Effects;

using TablectionSketch.Data;

namespace TablectionSketch.Controls
{
    public class TouchableImage : Image
    {
        public TransformGroup transformGroup = new TransformGroup();
        public TranslateTransform traslation = new TranslateTransform(0, 0);
        public ScaleTransform scale = new ScaleTransform(1, 1);
        public RotateTransform rotate = new RotateTransform(0);
        
        public TouchableImage()
        {           
            this.Cursor = Cursors.SizeAll;
            this.ForceCursor = true;

            this.Effect = new DropShadowEffect() { ShadowDepth = 0, BlurRadius = 5 };

            this.IsManipulationEnabled = true;

            this.transformGroup.Children.Add(this.rotate);
            this.transformGroup.Children.Add(this.scale);
            this.transformGroup.Children.Add(this.traslation);

            this.RenderTransform = this.transformGroup;
        }


        //Point _oldMousePoint;

        //protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    _oldMousePoint = e.GetPosition(null);

        //    this.SetSelected(true);

        //    base.OnMouseDown(e);

        //    e.Handled = true;
        //}


        //protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        //{
        //    Point currentMousePoint = e.GetPosition(null);

        //    if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        //    {
        //        Point mouseDelta = new Point(currentMousePoint.X - _oldMousePoint.X, currentMousePoint.Y - _oldMousePoint.Y);

        //        Point currentPosition = this.GetPosition();
        //        Point newPosition = new Point(currentPosition.X + mouseDelta.X, currentPosition.Y + mouseDelta.Y);
        //        this.SetPosition(newPosition);

        //        System.Diagnostics.Debug.WriteLine(string.Format("x;{0} y:{1}", newPosition.X, newPosition.Y));
        //    }

        //    this._oldMousePoint = currentMousePoint;

        //    base.OnMouseMove(e);

        //    e.Handled = true;
        //}


        private Point GetPosition()
        {
            double left = (double)this.GetValue(InkCanvas.LeftProperty);
            double top = (double)this.GetValue(InkCanvas.TopProperty);

            return new Point(left, top);
        }

        private void SetPosition(Point pt)
        {
            this.SetValue(InkCanvas.LeftProperty, pt.X);
            this.SetValue(InkCanvas.TopProperty, pt.Y);
        }

    }
}
