using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GongSolutions.Wpf.DragDrop;
using System.ComponentModel;

namespace TablectionSketch.Controls
{
    public class ImageSearchWindowVM : INotifyPropertyChanged, IDragSource  
    {




        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion



        public void StartDrag(DragInfo dragInfo)
        {
            throw new NotImplementedException();
        }
    }
}
    