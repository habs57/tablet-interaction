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

namespace Tablection
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        protected override void OnPreviewTouchMove(TouchEventArgs e)
        {

            //Desktop.IconInfoBrowser win = new Desktop.IconInfoBrowser();
            //win.Show();     // modeless
            //if (win.ShowDialog() == true)       // model
            //{
            //}
            
            TouchPointCollection collection = e.TouchDevice.GetIntermediateTouchPoints(this);

            if (lstDebug.Items.Count > 50)
            {
                lstDebug.Items.Clear();
            }

            foreach (var item in collection)
            {
                lstDebug.Items.Insert(0, string.Format("pt {0} - Time: {1}  X:{2} Y{3}", item.TouchDevice.Id, e.Timestamp.ToString(), item.Position.X, item.Position.Y));
            }

            base.OnPreviewTouchMove(e);
            
            /*
            TouchPointCollection collection = e.TouchDevice.GetIntermediateTouchPoints(this);
            
            foreach (var item in collection)
            {
                //item.Po
            }
             */
        }

    }
}
