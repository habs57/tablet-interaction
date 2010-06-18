using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Ink;

using System.Windows.Media;

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
                
        private ObservableCollection<TouchableItem> _objectCanvas = null;
        public ObservableCollection<TouchableItem> Objects
        {
            get
            {
                if (this._objectCanvas == null)
                {
                    this._objectCanvas = new ObservableCollection<TouchableItem>();                
                }
                return this._objectCanvas;
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
