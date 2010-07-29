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
    public class MainWindowVM : INotifyPropertyChanged
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
            string[] files = Directory.GetFiles(folderPath, "*.*");
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

        
    }
}
