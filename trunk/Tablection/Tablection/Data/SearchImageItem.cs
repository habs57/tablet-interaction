using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TablectionSketch.Data
{
    public class SearchImageItem : INotifyPropertyChanged
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set 
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
        

        private string _thumb;
        public string Thumbnail
        {
            get { return _thumb; }
            set 
            {
                _thumb = value;
                RaisePropertyChanged("Thumbnail");
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
