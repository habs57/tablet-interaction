using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Ink;

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TablectionSketch.Controls;
using TablectionSketch.Data;


namespace TablectionSketch.Slide
{
    public class Slide : INotifyPropertyChanged
    {
        /// <summary>
        /// 슬라이드의 이름
        /// </summary>
        private string _title = "이름없음";
        public string Title
        {
            get { return _title; }
            set 
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
        

        /// <summary>
        /// 슬라이드의 이미지가 있는 Uri
        /// </summary>
        private string _image;
        public string Image
        {
            get { return _image; }
            set 
            {
                _image = value;
                RaisePropertyChanged("Image");

                if (this.Thumbnail == null)
                {
                    this.Thumbnail = this.LoadThumbnail(_image);                    
                }                
            }
        }

        private BitmapImage LoadThumbnail(string path)
        {   
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();

            bitmapImage.UriSource = new Uri(path, UriKind.Relative);
            bitmapImage.CacheOption = BitmapCacheOption.OnDemand;

            bitmapImage.EndInit();

            bitmapImage.DecodePixelHeight = bitmapImage.PixelHeight >> 2;
            bitmapImage.DecodePixelWidth = bitmapImage.PixelWidth >> 2;            

            return bitmapImage;
        }
        
        /// <summary>
        /// 썸네일 이미지
        /// </summary>
        private ImageSource _thumnail = null;
        public ImageSource Thumbnail
        {
            get { return _thumnail; }
            set 
            { 
                _thumnail = value;
                RaisePropertyChanged("Thumbnail");
            }
        }

        /// <summary>
        /// 펜으로 그린 데이터 
        /// </summary>
        private StrokeCollection _strokes = null;
        public StrokeCollection Strokes
        {
            get
            {
                if (this._strokes == null)
                {
                    this._strokes = new StrokeCollection();
                }
                return this._strokes;
            }
        }

        private ObservableCollection<UIElement> _children = null;
        public ObservableCollection<UIElement> Children
        {
            get
            {
                if (this._children == null)
                {
                    this._children = new ObservableCollection<UIElement>();
                }
                return this._children;
            }
        }


        #region INotifyPropertyChanged Members

        /// <summary>
        /// 속성이 바뀌면 이를 연결된 컨트롤등에 알려준다.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
