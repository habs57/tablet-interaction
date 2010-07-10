using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TablectionSketch.Controls.LoopPanel
{
    internal class SelectHitTestHelper
    {
        private UIElement _hitTestRoot = null;

        public SelectHitTestHelper(UIElement hitTestRoot)
        {
            this._hitTestRoot = hitTestRoot;
        }

        private HitTestFilterCallback _selectFilter = null;
        public HitTestFilterCallback SelectHitTestFilter 
        { 
            get
            {
                if (_selectFilter == null)
	            {
                    this._selectFilter = new HitTestFilterCallback(this.SelectFilter);		 
	            }
                return this._selectFilter;
            }
        }

        private HitTestResultCallback _selectResult = null;
        public HitTestResultCallback SelectHitTestResult 
        {
            get
            {
                if (_selectResult == null)
                {
                    this._selectResult = new HitTestResultCallback(this.SelectResult);
                }
                return this._selectResult;
            }            
        }

        #region Visual Test Callback

        private HitTestFilterBehavior SelectFilter(DependencyObject o)
        {
            ListBoxItem item = o as ListBoxItem;
            if (item != null)
            {
                item.IsSelected = true;
                return HitTestFilterBehavior.Stop;
            }

            return HitTestFilterBehavior.Continue;
        }

        private HitTestResultBehavior SelectResult(HitTestResult o)
        {
            return HitTestResultBehavior.Stop;
        }

        #endregion

        public void SelectItemAt(Point pt)
        {
            PointHitTestParameters param = new PointHitTestParameters(pt);
            
            VisualTreeHelper.HitTest(this._hitTestRoot,
                                     this.SelectHitTestFilter,
                                     this.SelectHitTestResult,
                                     param);
        }
    }
}
