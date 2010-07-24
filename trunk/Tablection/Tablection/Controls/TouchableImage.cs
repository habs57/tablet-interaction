﻿using System;
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
        private TransformGroup _transformGroup;
        
        public TouchableImage()
        {
            this.Cursor = Cursors.SizeAll;
            this.ForceCursor = true;

            this.Effect = new DropShadowEffect() { ShadowDepth = 0, BlurRadius = 5 };

            this.SetValue(UIElement.IsManipulationEnabledProperty, true);
        }


        protected override void OnManipulationStarting(System.Windows.Input.ManipulationStartingEventArgs e)
        {           
            e.ManipulationContainer = this;
            e.Handled = true;

            this.SetSelected(true);
            //base.OnManipulationStarting(e);
        }

        protected override void OnManipulationDelta(System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            double transX = e.DeltaManipulation.Translation.X;
            double transY = e.DeltaManipulation.Translation.Y;

            //Rectangle rectToMove = e.OriginalSource as Rectangle;
            //Matrix rectsMatrix = ((MatrixTransform)rectToMove.RenderTransform).Matrix;

            //rectsMatrix.RotateAt(e.DeltaManipulation.Rotation,
            //                     e.ManipulationOrigin.X,
            //                     e.ManipulationOrigin.Y);

            //rectsMatrix.ScaleAt(e.DeltaManipulation.Scale.X,
            //                    e.DeltaManipulation.Scale.X,
            //                    e.ManipulationOrigin.X,
            //                    e.ManipulationOrigin.Y);

            //rectsMatrix.Translate(e.DeltaManipulation.Translation.X,
            //                      e.DeltaManipulation.Translation.Y);

            //rectToMove.RenderTransform = new MatrixTransform(rectsMatrix);

            //Rect containingRect =
            //    new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

            //Rect shapeBounds = this.Tra
            //    rectToMove.RenderTransform.TransformBounds(
            //        new Rect(rectToMove.RenderSize));

            //if (e.IsInertial && !containingRect.Contains(shapeBounds))
            //{
            //    e.Complete();
            //}

            Point pt = this.GetPosition();
            this.SetPosition(new Point(pt.X + transX, pt.Y + transY));

            e.Handled = true;
            //base.OnManipulationDelta(e);
        }

        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            base.OnManipulationCompleted(e);
        }

        protected override void OnManipulationInertiaStarting(System.Windows.Input.ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);

            e.ExpansionBehavior.DesiredDeceleration = 1.0 * 96 / (1000.0 * 1000.0);

            e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);

            e.Handled = true;
            //base.OnManipulationInertiaStarting(e);
        }


        Point _oldMousePoint;

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            _oldMousePoint = e.GetPosition(null);

            this.SetSelected(true);

            base.OnMouseDown(e);

            e.Handled = true;
        }


        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            Point currentMousePoint = e.GetPosition(null);

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Point mouseDelta = new Point(currentMousePoint.X - _oldMousePoint.X, currentMousePoint.Y - _oldMousePoint.Y);

                Point currentPosition = this.GetPosition();
                Point newPosition = new Point(currentPosition.X + mouseDelta.X, currentPosition.Y + mouseDelta.Y);
                this.SetPosition(newPosition);

                System.Diagnostics.Debug.WriteLine(string.Format("x;{0} y:{1}", newPosition.X, newPosition.Y));
            }

            this._oldMousePoint = currentMousePoint;

            base.OnMouseMove(e);

            e.Handled = true;
        }

        private void SetSelected(bool flag)
        {
             TouchableItem item = this.DataContext as TouchableItem;
             if (item != null)
             {
                 item.IsSelected = flag;
             }
        }

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

        ~TouchableImage()
        {
            System.Diagnostics.Debug.WriteLine("Destruct Touch Object");
        }
    }
}
