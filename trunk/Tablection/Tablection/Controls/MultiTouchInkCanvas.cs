using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;

namespace TablectionSketch.Controls
{
    public class MultiTouchInkCanvas : InkCanvas 
    {
        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
        }

        protected override void OnTouchMove(System.Windows.Input.TouchEventArgs e)
        {
            int id = e.TouchDevice.Id;
            TouchPoint touchPoint = e.GetTouchPoint(this);

            System.Diagnostics.Debug.WriteLine(string.Format("id:{0} x:{1} y:{2}", id, touchPoint.Position.X, touchPoint.Position.Y));

            base.OnTouchMove(e);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {

            base.OnTouchUp(e);
        }
    }
}
