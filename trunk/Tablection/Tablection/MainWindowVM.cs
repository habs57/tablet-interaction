using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.ComponentModel;

using GongSolutions.Wpf.DragDrop;

using TablectionSketch.Tool;
using TablectionSketch.Controls;


namespace TablectionSketch
{
    public class MainWindowVM : INotifyPropertyChanged, IDropTarget
    {
        private object _selectedSlide = null;
        public object SelectedSlide
        {
            get
            {
                return _selectedSlide;
            }
            set
            {
                _selectedSlide = value;
                this.NotifyPropertyChanged("SelectedSlide");
            }
        }


        private object _selectedTool = null;    
        public object SelectedTool
        {
            get 
            { 
                return _selectedTool; 
            }
            set 
            { 
                _selectedTool = value;
                this.NotifyPropertyChanged("SelectedTool");
            }
        }

                

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region IDropTarget Members

        public void DragOver(DropInfo dropInfo)
        {
            MultiTouchInkCanvas element = dropInfo.VisualTarget as MultiTouchInkCanvas;
            DataObject data = dropInfo.Data as DataObject;
            if (element != null && data != null && this.SelectedSlide != null)
            {
                dropInfo.Effects = DragDropEffects.Move | DragDropEffects.Copy;
            }
        }

        public void Drop(DropInfo dropInfo)
        {
            MultiTouchInkCanvas target = dropInfo.VisualTarget as MultiTouchInkCanvas;
            DataObject data = dropInfo.Data as DataObject;
            TablectionSketch.Slide.Slide silde = this.SelectedSlide as TablectionSketch.Slide.Slide;
            if (target != null && data != null && silde != null)
            {                
                System.Collections.Specialized.StringCollection fileNames = data.GetFileDropList();
                foreach (var item in fileNames)
                {
                    BitmapImage bmp = new BitmapImage(new Uri(item));
                    bmp.CacheOption = BitmapCacheOption.OnDemand;

                    Image img = new Image() { Source = bmp };

                    TablectionSketch.Data.TouchableItem tobj = new TablectionSketch.Data.TouchableItem() { Width = bmp.PixelWidth, Height = bmp.PixelHeight, Child = img };
                    silde.Objects.Add(tobj);                    
                }
            }
        }

        #endregion
    }
}
