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

        private void Grid_TouchMove(object sender, TouchEventArgs e)
        {
            Point pt = e.GetTouchPoint(null).Position;

            double deltaX = pt.X - _prevPoint.X;
            double deltaY = pt.Y - _prevPoint.Y;

            this.Left += deltaX;
            this.Top += deltaY;

            _prevPoint = e.GetTouchPoint(null).Position;
        }

        private void Grid_TouchDown(object sender, TouchEventArgs e)
        {
            _prevPoint = e.GetTouchPoint(null).Position;
        }

    }
}
