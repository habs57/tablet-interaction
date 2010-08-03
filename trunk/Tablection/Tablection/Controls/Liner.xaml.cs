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
        private TransformGroup transformGroup;
        TranslateTransform translation;
        RotateTransform rotation;

        public Liner()
        {
            InitializeComponent();

            this.ManipulationStarting += this.TouchableThing_ManipulationStarting;
            this.ManipulationDelta += this.TouchableThing_ManipulationDelta;
            //this.ManipulationInertiaStarting += this.TouchableThing_ManipulationInertiaStarting;

            this.transformGroup = new TransformGroup();

            this.translation = new TranslateTransform(0, 0);
            this.rotation = new RotateTransform(0);

            this.transformGroup.Children.Add(this.rotation);
            this.transformGroup.Children.Add(this.translation);

            this.LineRuler.RenderTransform = this.transformGroup;
        }


        void TouchableThing_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
        }

        void TouchableThing_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {            
            // Get the Rectangle and its RenderTransform matrix.
            Rectangle rectToMove = new Rectangle();// e.OriginalSource as Rectangle;
            Matrix rectsMatrix = ((MatrixTransform)rectToMove.RenderTransform).Matrix;

            // Rotate the Rectangle.
            rectsMatrix.RotateAt(e.DeltaManipulation.Rotation,
                                 e.ManipulationOrigin.X,
                                 e.ManipulationOrigin.Y);

            // Resize the Rectangle.  Keep it square 
            // so use only the X value of Scale.
            rectsMatrix.ScaleAt(e.DeltaManipulation.Scale.X,
                                e.DeltaManipulation.Scale.X,
                                e.ManipulationOrigin.X,
                                e.ManipulationOrigin.Y);

            // Move the Rectangle.
            rectsMatrix.Translate(e.DeltaManipulation.Translation.X,
                                  e.DeltaManipulation.Translation.Y);

            this.LineRuler.Width *= e.DeltaManipulation.Scale.X;
            this.LineRuler.Height *= e.DeltaManipulation.Scale.Y;

            // the center never changes in this sample, although we always compute it.
            Point center = new Point(this.LineRuler.RenderSize.Width / 2.0, this.LineRuler.RenderSize.Height / 2.0);

            // apply the rotation at the center of the rectangle if it has changed
            this.rotation.CenterX = center.X;
            this.rotation.CenterY = center.Y;
            this.rotation.Angle += e.DeltaManipulation.Rotation;

            // apply translation 
            this.translation.X += e.DeltaManipulation.Translation.X;
            this.translation.Y += e.DeltaManipulation.Translation.Y;


            // Apply the changes to the Rectangle.
            rectToMove.RenderTransform = new MatrixTransform(rectsMatrix);

            e.Handled = true;
        }

        //void TouchableThing_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        //{
        //    e.TranslationBehavior = new InertiaTranslationBehavior();
        //    e.TranslationBehavior.InitialVelocity = e.InitialVelocities.LinearVelocity;
        //    // 10 inches per second squared
        //    e.TranslationBehavior.DesiredDeceleration = 10 * 96 / (1000 * 1000);


        //    e.ExpansionBehavior = new InertiaExpansionBehavior();
        //    e.ExpansionBehavior.InitialVelocity = e.InitialVelocities.ExpansionVelocity;
        //    // .1 inches per second squared.
        //    e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / 1000.0 * 1000.0;

        //    e.RotationBehavior = new InertiaRotationBehavior();
        //    e.RotationBehavior.InitialVelocity = e.InitialVelocities.AngularVelocity;
        //    // 720 degrees per second squared.
        //    e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);
        //}

        private void DrawGradations()
        {
            int x = 10;

            while (x < this.ActualWidth)
            {
                Line line = new Line();
                line.Stroke = Brushes.Gray;
                line.StrokeThickness = 2.0;
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
