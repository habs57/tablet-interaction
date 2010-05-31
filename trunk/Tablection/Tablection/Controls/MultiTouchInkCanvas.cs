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

            e.Handled = true;
        }

        protected override void OnTouchMove(System.Windows.Input.TouchEventArgs e)
        {
            int id = e.TouchDevice.Id;
            TouchPoint touchPoint = e.GetTouchPoint(this);

            System.Diagnostics.Debug.WriteLine(string.Format("id:{0} x:{1} y:{2}", id, touchPoint.Position.X, touchPoint.Position.Y));

            base.OnTouchMove(e);

            e.Handled = true;
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);

            e.Handled = true;
        }

        //protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
        //{
        //    System.Diagnostics.Debug.WriteLine("ArrangeOverride");
        //    return base.ArrangeOverride(arrangeSize);
        //}

        //protected override Geometry GetLayoutClip(System.Windows.Size layoutSlotSize)
        //{
        //    System.Diagnostics.Debug.WriteLine("GetLayoutClip");
        //    return base.GetLayoutClip(layoutSlotSize);
        //}

        //protected override System.Windows.DependencyObject GetUIParentCore()
        //{
        //    System.Diagnostics.Debug.WriteLine("GetUIParentCore");
        //    return base.GetUIParentCore();
        //}

        //protected override Visual GetVisualChild(int index)
        //{
        //    System.Diagnostics.Debug.WriteLine("GetVisualChild");
        //    return base.GetVisualChild(index);
        //}

        //protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
        //{
        //    System.Diagnostics.Debug.WriteLine("HitTestCore - Geometry");
        //    return base.HitTestCore(hitTestParameters);
        //}

        //protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParams)
        //{
        //    System.Diagnostics.Debug.WriteLine("HitTestCore - Point");
        //    return base.HitTestCore(hitTestParams);
        //}

        //protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        //{
        //    System.Diagnostics.Debug.WriteLine("MeasureOverride");
        //    return base.MeasureOverride(availableSize);
        //}

        //protected override void OnChildDesiredSizeChanged(System.Windows.UIElement child)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnChildDesiredSizeChanged");
        //    base.OnChildDesiredSizeChanged(child);
        //}

        //protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
        //{
        //    System.Diagnostics.Debug.WriteLine("OnCreateAutomationPeer");
        //    return base.OnCreateAutomationPeer();
        //}

        //protected override void OnInitialized(EventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnInitialized");
        //    base.OnInitialized(e);
        //}

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnRender");
        //    base.OnRender(drawingContext);
        //}

        //protected override void OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnRenderSizeChanged");
        //    base.OnRenderSizeChanged(sizeInfo);
        //}

        //protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnVisualChildrenChanged");
        //    base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        //}

        //protected override void ParentLayoutInvalidated(System.Windows.UIElement child)
        //{
        //    System.Diagnostics.Debug.WriteLine("ParentLayoutInvalidated");
        //    base.ParentLayoutInvalidated(child);
        //}

        //protected override void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnVisualParentChanged");
        //    base.OnVisualParentChanged(oldParent);
        //}
    }
}
