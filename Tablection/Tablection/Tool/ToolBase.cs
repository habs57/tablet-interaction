using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace TablectionSketch.Tool
{
    public abstract class ToolBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 툴 이름
        /// </summary>
        private string _name = "이름없음";
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// 툴 아이콘 이미지
        /// </summary>
        private string _iconImage = null;
        public string Icon
        {
            get { return _iconImage = null;; }
            set 
            { 
                _iconImage = value;
                RaisePropertyChanged("Icon");
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
