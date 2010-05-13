using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tablection.Desktop
{
    public class FolderIcon : ICon
    {
        public FolderIcon()
        {
            //this.Background = new ImageBrush(new BitmapImage(new Uri(@"..\Images\Stuffed_Folder.png")));
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }
    }
}
