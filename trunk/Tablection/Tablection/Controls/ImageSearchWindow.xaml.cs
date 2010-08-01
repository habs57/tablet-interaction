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
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.Web;
using System.Xml;
using System.Windows.Interop;

using System.Collections.ObjectModel;

using TablectionSketch.Data;

namespace TablectionSketch.Controls
{
    /// <summary>
    /// Interaction logic for ImageSearchWindow.xaml
    /// </summary>
    public partial class ImageSearchWindow : Window
    {
        public ImageSearchWindow()
        {
            InitializeComponent();

            //this.lstImages.ItemsSource = this.Results;

            //Set the ListView View property to the tileView custom view
            //this.lstImages.View = this.FindResource("tileView") as ViewBase;
        }
          
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string key = "6c0d878a7ee957dcebf084ccfd91ebd0";
            string query = HttpUtility.UrlEncode(this.txtSearch.Text, Encoding.GetEncoding("utf-8")); 
            string request = string.Format("http://openapi.naver.com/search?key={0}&query={1}&target=image&start=1&display=20", key, query);
            WebRequest req = HttpWebRequest.Create(request);
            using (WebResponse response = req.GetResponse())
            {
                Stream strm = response.GetResponseStream();
                StreamReader reader = new StreamReader(strm, Encoding.UTF8);
                string data = reader.ReadToEnd();

                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(data);
                xdoc.Save("result.xml");

                XmlDataProvider provider = this.FindResource("myXmlDataBase") as XmlDataProvider;
                if (provider != null)
                {
                    provider.Document = xdoc;
                }
            }
        }

        private void lstImages_DragEnter(object sender, DragEventArgs e)
        {

        }

        private Point _prevPoint;

        private void Grid_PreviewTouchDown(object sender, TouchEventArgs e)
        {
           ////_prevPoint = e.GetTouchPoint(this.grdBase).Position;
           // this.DragMove();
           // e.Handled = true;
        }

        private void Grid_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            //Point pt = e.GetTouchPoint(this.grdBase).Position;

            //double deltaX = pt.X - _prevPoint.X;
            //double deltaY = pt.Y - _prevPoint.Y;

            //this.Left += deltaX;
            //this.Top += deltaY;

            //_prevPoint = pt;
        }

        private void Window_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            
            //this.DragMove();
            //e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get the window and add a message hook.
            //IntPtr hwnd = new WindowInteropHelper(this).Handle;
            //HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = this.IsCancel;
            if (this.IsCancel == true)
            {
                this.Hide();                
            }            
        }

        private bool IsCancel = true;

        public void KillMe()
        {
            this.IsCancel = false;
            this.Close();
        }

        //private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    if (msg == WM_NCHITTEST)
        //    {
        //        // get screen coordinates  
        //        Point p = new Point();
        //        int pInt = lParam.ToInt32();
        //        p.X = (pInt << 16) >> 16; // lo order word  
        //        p.Y = pInt >> 16; // hi order word  
        //        if (IsOnNoncontainerControl(this, p) == false)
        //        {
        //            //The point is not in a noncontainer control(like TextBox or Button),
        //            //In other words, it is on a Panel, a Grid or the other container element.
        //            //So we lie the system that the point is on the caption bar.
        //            handled = true;
        //            return new IntPtr(2);
        //        }
        //    }

        //    return IntPtr.Zero;
        //}

        ////Check if the point is on a noncontainer control.
        //private bool IsOnNoncontainerControl(FrameworkElement parent, Point p)
        //{
        //    IInputElement ctrl = parent.InputHitTest(parent.PointFromScreen(p));
        //    if (ctrl != null && ctrl != parent && ctrl is FrameworkElement)
        //    {
        //        if ((ctrl is Panel) || (ctrl is Decorator))
        //        {
        //            return IsOnNoncontainerControl(ctrl as FrameworkElement, p);
        //        }
        //        else
        //        {
        //            Rect r = GetBoundingBox(ctrl as FrameworkElement, parent);
        //            r.Location = parent.PointToScreen(r.Location);
        //            if (r.Contains(p))
        //                return true;
        //            else
        //                return false;
        //        }
        //    }
        //    else
        //        return false;
        //}

        ////Get the bounds of a element.
        //private Rect GetBoundingBox(FrameworkElement element, FrameworkElement containerWindow)
        //{
        //    GeneralTransform transform = element.TransformToAncestor(containerWindow);
        //    Point topLeft = transform.Transform(new Point(0, 0));
        //    Point bottomRight = transform.Transform(new Point(element.ActualWidth, element.ActualHeight));
        //    return new Rect(topLeft, bottomRight);
        //}

        //private const int WM_NCHITTEST = 0x0084;
    }
}
