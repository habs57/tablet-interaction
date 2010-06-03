using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TablectionSketch.Controls
{
    public class TouchObject : Canvas
    {
        public TouchObject()
        {
            this.SetValue(TouchObject.IsManipulationEnabledProperty, true);
        }

        protected override void OnManipulationStarting(System.Windows.Input.ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            e.Handled = true;
            //base.OnManipulationStarting(e);
        }

        protected override void OnManipulationStarted(System.Windows.Input.ManipulationStartedEventArgs e)
        {
            base.OnManipulationStarted(e);
        }

        protected override void OnManipulationDelta(System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
        }

        protected override void OnManipulationCompleted(System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            base.OnManipulationCompleted(e);
        }
    }
}
