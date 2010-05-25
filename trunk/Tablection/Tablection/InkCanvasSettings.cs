using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Ink;
using System.Windows.Controls;
using System.ComponentModel;

namespace TablectionSketch
{
    public class InkCanvasSettings : INotifyPropertyChanged 
    {
        private DrawingAttributes _drawingAttributes = new DrawingAttributes();
        public DrawingAttributes DrawingAttributes
        {
            get { return _drawingAttributes; }
            set 
            { 
                _drawingAttributes = value;
                RaisePropertyChanged("DrawingAttributes");
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
