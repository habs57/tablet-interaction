using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;
using System.ComponentModel;

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
