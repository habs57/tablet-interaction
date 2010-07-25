﻿using System;
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

using System.Windows.Ink;
using System.Windows.Threading;
using System.Windows.Media.Animation;

using TablectionSketch.Tool;
using TablectionSketch.Controls;

namespace TablectionSketch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            this.DrawingCanvas.Gesture += new RecongnitionGestrueHandler(DrawingCanvas_Gesture);
        }

        void DrawingCanvas_Gesture(ApplicationGesture Gestrue)
        {
            System.Diagnostics.Debug.WriteLine(Gestrue.ToString());
        }
        
        private void btnBottom_Click(object sender, RoutedEventArgs e)
        {        
            this.SlideList.Visibility = Visibility.Visible;       
        }

        private void RefreshCurrentPreview()
        {
            Slide.Slide currentSlide = this.SlideList.SelectedItem as Slide.Slide;
            if (currentSlide != null)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.DrawingCanvas.ActualWidth, (int)this.DrawingCanvas.ActualHeight, 100, 100, PixelFormats.Default);                
                rtb.Render(this.DrawingCanvas);

                currentSlide.Thumbnail = rtb;
            }
        }

        public RenderTargetBitmap SaveControl(FrameworkElement Target)
        {

            FrameworkElement Parent = Target.Parent as FrameworkElement;
            double ParentWidth, ParentHeight;

            if (Parent == null)
            {
                ParentWidth = 1000;
                ParentHeight = 1000;

                Canvas ParentCanvas = new Canvas();
                ParentCanvas.Children.Add(Target);
                Parent = ParentCanvas;
            }
            else
            {
                ParentWidth = Parent.ActualWidth;
                ParentHeight = Parent.ActualHeight;
            }


            Target.Measure(new Size(ParentWidth, ParentHeight));
            Target.Arrange(new Rect(0, 0, ParentWidth, ParentHeight));

            Target.Measure(new Size(Target.ActualWidth, Target.ActualHeight));
            Target.Arrange(new Rect(0, 0, Target.ActualWidth, Target.ActualHeight));

            Rect Rect = Target.TransformToVisual(Parent).TransformBounds(new Rect(0, 0, Target.ActualWidth, Target.ActualHeight));


            Target.Arrange(new Rect(-Rect.Left, -Rect.Top, Target.ActualWidth, Target.ActualHeight));

            RenderTargetBitmap r = new RenderTargetBitmap((int)Rect.Width, (int)Rect.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            r.Render(Target);

            return r;

        }

        private void btnTop_Click(object sender, RoutedEventArgs e)
        {         
            this.ToolPanel.Visibility = Visibility.Visible;
        }
        
        private void SlideListHideAnimation_Completed(object sender, EventArgs e)
        {
            this.SlideList.Visibility = Visibility.Collapsed;
        }

        private void ToolPanelHideAnimaton_Completed(object sender, EventArgs e)
        {
            this.ToolPanel.Visibility = Visibility.Collapsed;
        }
        

        private void HeaderBasicTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioTools.IsChecked = true;
        }

        private void HeaderColorTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioColors.IsChecked = true;
        }

        private void HeaderStrokeTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioStrokes.IsChecked = true;
        }
        
        //private void SlideList_TouchEnter(object sender, TouchEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("SlideList_TouchEnter");
        //}

        //private void SlideList_TouchLeave(object sender, TouchEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("SlideList_TouchLeave");
        //}
    
        //private void SlideList_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("SlideList_MouseEnter");
        //}

        //private void SlideList_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("SlideList_MouseLeave");
        //}

        //private void ToolPanel_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("ToolPanel_MouseEnter");
        //}

        //private void ToolPanel_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("ToolPanel_MouseLeave");
        //}

        //private void ToolPanel_TouchEnter(object sender, TouchEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("ToolPanel_TouchEnter");
        //}

        //private void ToolPanel_TouchLeave(object sender, TouchEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("ToolPanel_TouchLeave");
        //}

        private ImageSearchWindow _searchWindow = null;
        protected ImageSearchWindow SearchWindow
        {
            get
            {
                if (_searchWindow == null)
                {
                    _searchWindow = new ImageSearchWindow();
                }
                return _searchWindow;
            }
        }

        private void LoopingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("LoopingListBox_SelectionChanged");

            foreach (ToolHeader item in e.AddedItems)
            {
                string selectedToolName = item.RelatedControlName;
                if (this.radioTools != null && selectedToolName.Equals("llbTools") == true)
                {
                    this.radioTools.IsChecked = true;
                }
                else if (this.radioColors != null && selectedToolName.Equals("llbColors") == true)
                {
                    this.radioColors.IsChecked = true;
                }
                else if (this.radioStrokes != null && selectedToolName.Equals("llbStroke") == true)
                {
                    this.radioStrokes.IsChecked = true;
                }
                else if (this.radioStrokes != null && selectedToolName.Equals("ctl_Search") == true)
                {
                    //검색창 띄움
                    this.SearchWindow.Show();
                    
                }
                else
                {
                    this.radioTools.IsChecked = false;
                    this.radioColors.IsChecked = false;
                    this.radioStrokes.IsChecked = false;
                    this.SearchWindow.Hide();
                }
            }
        }

      

#region Button Top for Tools 
        
        private void btnTop_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {            
            //툴 스크롤
            //this.llbToolHeaders.Offset += e.DeltaManipulation.Translation.Length;
            //System.Diagnostics.Debug.WriteLine(string.Format("btnTop_ManipulationDelta : Offset - {0}, Trans - {1}",this.llbToolHeaders.Offset, e.DeltaManipulation.Translation.Length));
        }

        private void btnTop_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnTop_ManipulationStarted");
            this.ToolPanel.Visibility = Visibility.Visible;
        }

#endregion

#region Button Bottom for Tools 
                
        private void btnBottom_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            //페이지 넘기기 인터렉션
            int currentIndex = this.SlideList.SelectedIndex;
            int totalSlides = this.SlideList.Items.Count;

            double trans = e.DeltaManipulation.Translation.X;

            System.Diagnostics.Debug.WriteLine(string.Format("btnBottom_ManipulationDelta : X - {0}", trans));

        }

        private void btnBottom_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {            
            System.Diagnostics.Debug.WriteLine("btnBottom_ManipulationStarted");
            this.SlideList.Visibility = Visibility.Visible;   
        }
        
#endregion

        private void DrawingCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.RefreshCurrentPreview();
        }

        private void DrawingCanvas_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            //PreviewRefresh
            this.RefreshCurrentPreview();
        }

       
        private void DrawingCanvas_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            //펜을 캔버스에 대면 자동적으로 쓰기모드
            //this.llbTools.SelectedIndex = 1;        
        }

        private void DrawingCanvas_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            

        }

        private HitTestFilterBehavior FilterCallBack(DependencyObject e)
        {
            if (e is TouchableImage)
            {
                (e as TouchableImage).Focus();
                return HitTestFilterBehavior.Stop;
            }

            return HitTestFilterBehavior.Continue;
        }

        private HitTestResultBehavior ResultCallBack(HitTestResult e)
        {
            TouchableImage touchObj = e.VisualHit as TouchableImage;
            if (touchObj != null)
            {
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;
        }

        private TouchModeRecognizer _modeRecognizer = new TouchModeRecognizer();
        private void DrawingCanvas_TouchDown(object sender, TouchEventArgs e)
        {
            _modeRecognizer.Recognize(e);
            bool IsMultiTouch = _modeRecognizer.IsMultiTouch;
            if (IsMultiTouch == true)
            {
                
                //this.llbTools.SelectedIndex = 0;
                //VisualTreeHelper.HitTest(this, new HitTestFilterCallback(FilterCallBack), new HitTestResultCallback(ResultCallBack), new PointHitTestParameters(e.GetTouchPoint(null).Position));                
            }
            else
            {
                
                //this.llbTools.SelectedIndex = 5;
            }
        }

        private void DrawingCanvas_TouchUp(object sender, TouchEventArgs e)
        {
            
            //this.llbTools.SelectedIndex = 0;            
        }

        private void DrawingCanvas_StylusDown(object sender, StylusDownEventArgs e)
        {            
            //펜을 캔버스에 대면 자동적으로 쓰기모드
            
            //this.llbTools.SelectedIndex = 1;
        }

        private void BackupChildObj(SelectionChangedEventArgs e)
        {
            //이전거 백업 
            if (e.RemovedItems != null && e.RemovedItems.Count > 0)
            {
                Slide.Slide oldSlide = e.RemovedItems[0] as Slide.Slide;
                if (oldSlide != null)
                {
                    oldSlide.Children.Clear();
                    foreach (UIElement item in this.DrawingCanvas.Children)
                    {
                        oldSlide.Children.Add(cloneElement(item));
                    }
                    this.DrawingCanvas.Children.Clear();
                }
            }
        }

        private void RestoreChildObj(SelectionChangedEventArgs e)
        {
            //새거 로드
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                Slide.Slide newSlide = e.AddedItems[0] as Slide.Slide;
                if (newSlide != null)
                {
                    foreach (UIElement item in newSlide.Children)
                    {
                        this.DrawingCanvas.Children.Add(item);
                    }
                }
            }           
        }

        private void SlideList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.BackupChildObj(e);
            this.RestoreChildObj(e);
        }

        /// <summary>
        /// clones a UIElement
        /// </summary>
        /// <param name="orig"></param>
        /// <returns></returns>
        public static UIElement cloneElement(UIElement orig)
        {
            if (orig == null)
            {
                return (null);
            }

            var s = System.Windows.Markup.XamlWriter.Save(orig);
            var stringReader = new System.IO.StringReader(s);
            var xmlReader = System.Xml.XmlTextReader.Create(stringReader, new System.Xml.XmlReaderSettings());

            return (UIElement)System.Windows.Markup.XamlReader.Load(xmlReader);

        }

        #region 드래그드롭 기능 

        private string[] supportedFormats = new string[] { ".jpg", ".bmp", ".gif", ".png" };

        private void DrawingCanvas_DragOver(object sender, DragEventArgs e)
        {
            DoWithSupportedImage(e, (path, args) => 
            {
                string extension = System.IO.Path.GetExtension(path).ToLower();
                bool isSupportedFormat = supportedFormats.Contains(extension);
                if (isSupportedFormat == true)
                {
                    args.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                }
            });           
        }

        private void DrawingCanvas_Drop(object sender, DragEventArgs e)
        {
            DoWithSupportedImage(e, (path, args) =>
            {
                BitmapImage image = new BitmapImage(new Uri(path));
                TouchableImage ti = new TouchableImage();
                ti.Source = image;
                ti.Width = (double)(image.PixelWidth >> 1);
                ti.Height = (double)(image.PixelHeight >> 1);
                this.DrawingCanvas.Children.Add(ti);

                Point pt = e.GetPosition(this.DrawingCanvas);
                InkCanvas.SetLeft(ti, pt.X - (image.PixelWidth >> 2));
                InkCanvas.SetTop(ti, pt.Y - (image.PixelHeight >> 2));
            });           
        }

        private void DoWithSupportedImage(DragEventArgs e, Action<string, DragEventArgs> doAction)
        {
            bool isFileExist = e.Data.GetDataPresent("FileNameW");
            if (isFileExist == true)
            {
                string[] path = e.Data.GetData("FileNameW") as string[];
                if (path != null && path.Length > 0)
                {
                    string file = path[0];
                    doAction(file, e);
                }
            }

        }

        #endregion

        private void DrawingCanvas_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior = new System.Windows.Input.InertiaTranslationBehavior();
            e.TranslationBehavior.InitialVelocity = e.InitialVelocities.LinearVelocity;
            e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);

            e.ExpansionBehavior = new System.Windows.Input.InertiaExpansionBehavior();
            e.ExpansionBehavior.InitialVelocity = e.InitialVelocities.ExpansionVelocity;
            e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);

            e.RotationBehavior = new InertiaRotationBehavior();
            e.RotationBehavior.InitialVelocity = e.InitialVelocities.AngularVelocity;
            e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);
        }

        private void DrawingCanvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            TouchableImage image = e.OriginalSource as TouchableImage;
            if (image != null)
            {
                Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);
                Rect shapeBounds = image.transformGroup.TransformBounds(new Rect(image.RenderSize));

                if (e.IsInertial && !containingRect.Contains(shapeBounds))
                {
                    e.Complete();
                }

                Point center = new Point(image.RenderSize.Width / 2.0, image.RenderSize.Height / 2.0);

                //rotation
                image.rotate.CenterX = center.X;
                image.rotate.CenterY = center.Y;
                image.rotate.Angle += e.DeltaManipulation.Rotation;

                //scale
                image.scale.CenterX = center.X;
                image.scale.CenterY = center.Y;
                image.scale.ScaleX *= e.DeltaManipulation.Scale.X;
                image.scale.ScaleY *= e.DeltaManipulation.Scale.Y;

                //trans
                image.traslation.X += e.DeltaManipulation.Translation.X;
                image.traslation.Y += e.DeltaManipulation.Translation.Y;  
            }
            
        }

        private void DrawingCanvas_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;  
        }

        private void llbTools_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                BasicTool tool = e.AddedItems[0] as BasicTool;
                if (tool != null)
                {
                    this.DrawingCanvas.EditingMode = tool.Mode;
                }                
            }
            
        }

    }
}