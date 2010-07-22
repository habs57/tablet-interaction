using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Controls;
using System.Windows.Input;

namespace RealCrop
{
    public class TouchableImage : Image
    {
        private bool _mouseDown;
        private Point _point;

        public TouchableImage()
        {
            this.Cursor = Cursors.SizeAll;
            this.ForceCursor = true;

            this.Effect = new DropShadowEffect() { ShadowDepth = 0, BlurRadius = 3 }; 
        }

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            _mouseDown = true;

            _point = e.GetPosition(this);
            
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (_mouseDown)
            {
                Point point = e.GetPosition((IInputElement)Parent);
                point.Offset(-_point.X, -_point.Y);

                SetValue(InkCanvas.LeftProperty, point.X);
                SetValue(InkCanvas.TopProperty, point.Y);
            }
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            _mouseDown = false;

        }
       
    }
}
