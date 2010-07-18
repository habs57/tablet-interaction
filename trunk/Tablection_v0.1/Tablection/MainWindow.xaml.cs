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

using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Diagnostics;

using System.Windows.Threading;
using System.Windows.Media.Animation;

using TablectionSketch.Tool;

namespace TablectionSketch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public class UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }

    

    public partial class MainWindow : Window
    {

        private int gSensor_val;
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeUdpSocket();       
            
        }

        private void InitializeUdpSocket()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9985);
            UdpClient newsock = new UdpClient(ipep);

            UdpState s = new UdpState();
                        
            s.e = ipep;
            s.u = newsock;

            newsock.BeginReceive(new AsyncCallback(OnReceive), s);                        
        }
        
        public delegate void myDelegate(int val);

       
        public void OnReceive(IAsyncResult ar)
        {
            UdpState s = new UdpState();

            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            s.u = u;
            s.e = e;

            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            Debug.WriteLine(string.Format("Pen Signal Data : {0}",Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length)));

            if (Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length) == "NONE" )
            {
                gSensor_val = 1;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor),1);
                //this.llbTools.SelectedIndex = 1;
            }
            else if (Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length) == "WEAK")
            {
                gSensor_val = 2;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor), 2);
            }
            else if (Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length) == "STNG")
            {
                gSensor_val = 3;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor), 3);
            }
            else
            {
                gSensor_val = 4;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor), 4);
            }

            u.BeginReceive(new AsyncCallback(OnReceive), s);
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
        
        private void SlideList_TouchEnter(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_TouchEnter");
        }

        private void SlideList_TouchLeave(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_TouchLeave");
        }
    
        private void SlideList_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_MouseEnter");
        }

        private void SlideList_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SlideList_MouseLeave");
        }

        private void ToolPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_MouseEnter");
        }

        private void ToolPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_MouseLeave");
        }

        private void ToolPanel_TouchEnter(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_TouchEnter");
        }

        private void ToolPanel_TouchLeave(object sender, TouchEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ToolPanel_TouchLeave");
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
                else
                {
                    this.radioTools.IsChecked = false;
                    this.radioColors.IsChecked = false;
                    this.radioStrokes.IsChecked = false;
                }
            }
        }

        private void HeaderBasicTool_Selected(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("HeaderBasicTool_Selected");
        }

        private void HeaderColorTool_Selected(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("HeaderColorTool_Selected");
        }

        private void HeaderStrokeTool_Selected(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("HeaderStrokeTool_Selected");
        }

        private void llbStroke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("llbStroke_SelectionChanged");
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
            this.RefreshCurrentPreview();
        }

        


        private void DrawingCanvas_PreviewStylusDownBySensor(int val)
        {
            System.Diagnostics.Debug.WriteLine("Pen의 센서 데이터를 이용해서 쓰기모드");
            //펜을 캔버스에 대면 자동적으로 쓰기모드
            switch (val)
            {
                case 1:
                    this.llbTools.SelectedIndex = 4;
                    break;
                case 2:
                case 3:
                case 4:
                    this.llbTools.SelectedIndex = 1;
                    break;

            }
            
        }

        private void DrawingCanvas_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("DrawingCanvas_PreviewStylusDown [펜을 캔버스에 대면 자동적으로 쓰기모드]");
            //펜을 캔버스에 대면 자동적으로 쓰기모드
            //this.llbTools.SelectedIndex = 1;
        }

        private void DrawingCanvas_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("DrawingCanvas_PreviewTouchDown[손가락을 캔버스에 대면 자동적으로 선택모드]");
            //손가락을 캔버스에 대면 자동적으로 선택모드
            //this.llbTools.SelectedIndex = 4;
            if (gSensor_val == 1)
            {
                this.llbTools.SelectedIndex = 4;
            }
            else
            {
                this.llbTools.SelectedIndex = 1;
            }
        }

        private void DrawingCanvas_TouchDown(object sender, TouchEventArgs e)
        {
            
        }

        //private void DrawingCanvas_PreviewDrop(object sender, DragEventArgs e)
        //{
        //    this.RefreshCurrentPreview();
        //}
        
    }
}
