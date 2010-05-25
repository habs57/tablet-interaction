using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;

namespace TablectionSketch.Tool
{
    /// <summary>
    /// 컬러객체
    /// </summary>
    public class ColorTool : BasicTool
    {
        /// <summary>
        /// 컬러
        /// </summary>
        private Brush _color = Brushes.Black;
        public Brush Color
        {
            get { return _color; }
            set 
            { 
                _color = value;
                RaisePropertyChanged("Color");
            }
        }       
             
        
    }
}
