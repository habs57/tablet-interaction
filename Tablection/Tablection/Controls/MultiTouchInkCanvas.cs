using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;

namespace TablectionSketch.Controls
{
    public delegate void RecongnitionGestrueHandler(ApplicationGesture Gestrue);

    public class MultiTouchInkCanvas : InkCanvas 
    {
        public RecognitionConfidence Confidence { get; set; }
        public ObservableCollection<ApplicationGesture> EnabledGestures { get; private set; }
        public new event RecongnitionGestrueHandler Gesture;


        public MultiTouchInkCanvas()
        {
            EnabledGestures = new ObservableCollection<ApplicationGesture>();
            EnabledGestures.CollectionChanged += (S, E) => SetEnabledGestures(EnabledGestures);

            EnabledGestures.Add(ApplicationGesture.AllGestures);
        }


        protected override void OnGesture(InkCanvasGestureEventArgs e)
        {
            GestureRecognitionResult Result = e.GetGestureRecognitionResults()[0];

            if (Result.ApplicationGesture != ApplicationGesture.NoGesture && Result.RecognitionConfidence <= Confidence)
            {
                if (this.Gesture != null)
                {
                    Gesture(Result.ApplicationGesture);                    
                }
            }            
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnTouchMove(System.Windows.Input.TouchEventArgs e)
        {            
            e.Handled = true;
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            e.Handled = true;
        }

    }
}
