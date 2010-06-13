using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

using System.ComponentModel;

namespace TablectionSketch.Data
{
    public class TouchableItem : INotifyPropertyChanged
    {
        private double _width = 0;
        public double Width
        {
            get { return _width; }
            set 
            { 
                _width = value;
                this.RaisePropertyChanged("Width");
            }
        }

        private double _height = 0;
        public double Height
        {
            get { return _height; }
            set 
            { 
                _height = value;
                this.RaisePropertyChanged("Height");
            }
        }

        private double _x = 0;
        public double X
        {
            get { return _x; }
            set 
            { 
                _x = value;
                this.RaisePropertyChanged("X");
            }
        }

        private double _y = 0;
        public double Y
        {
            get { return _y; }
            set 
            { 
                _y = value;
                this.RaisePropertyChanged("Y");
            }
        }               

        private UIElement _child;
        public UIElement Child
        {
            get { return _child; }
            set 
            { 
                _child = value;
                this.RaisePropertyChanged("Child");
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
