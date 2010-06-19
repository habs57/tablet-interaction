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


using System.IO;

namespace TablectionSketch
{
    public class MainWindowVM : INotifyPropertyChanged, IDropTarget
    {
        public MainWindowVM()
        {
            FileInfo fi = new FileInfo(System.Reflection.Assembly.GetAssembly(typeof(MainWindowVM)).Location);
            DirectoryInfo[] di = fi.Directory.GetDirectories("Images");
            foreach (var item in di)
            {
                LoadSlides(item.FullName);               
            }            
        }

        private void LoadSlides(string folderPath)
        {            
            string[] files = Directory.GetFiles(folderPath, "*.jpg");
            foreach (var item in files)
            {
                FileInfo fi = new FileInfo(item);
                Slide.Slide slide = new Slide.Slide() { Image = item, Title = fi.Name };
                this.SlideCollection.Add(slide);
            }
        }

        private Slide.SlideCollcection _slideCollection = null;
        public Slide.SlideCollcection SlideCollection
        {
            get 
            {
                if (this._slideCollection == null)
                {
                    _slideCollection = new Slide.SlideCollcection();
                }            
                return _slideCollection; 
            }
            set 
            {
                _slideCollection = value;
                this.RaisePropertyChanged("SlideCollection");
            }
        }
        

        private Slide.Slide _selectedSlide = null;
        public Slide.Slide SelectedSlide
        {
            get
            {
                return _selectedSlide;
            }
            set
            {
                _selectedSlide = value;
                this.RaisePropertyChanged("SelectedSlide");
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
                this.RaisePropertyChanged("SelectedTool");
            }
        }

                

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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

                    Point pt = System.Windows.Input.Mouse.GetPosition(target);
                    TablectionSketch.Data.TouchableItem tobj = new TablectionSketch.Data.TouchableItem(this.SelectedSlide) { X = pt.X - (bmp.PixelWidth >> 1), Y = pt.Y - (bmp.PixelHeight >> 1),  Width = bmp.PixelWidth, Height = bmp.PixelHeight, Child = img };
                    silde.Objects.Add(tobj);                    
                }
            }
        }

        #endregion
    }
}
