using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TablectionSketch.Tool
{
    public class StrokeTool : ToolBase
    {
        /// <summary>
        /// 붓 너비크기 설정
        /// </summary>
        private double _width = 1;
        public double Width
        {
            get { return _width; }
            set 
            {
                _width = value;
                RaisePropertyChanged("Width");
            }
        }

        /// <summary>
        /// 붓 높이크기 설정
        /// </summary>
        private double _height = 1;
        public double Height
        {
            get { return _height; }
            set 
            { 
                _height = value;
                RaisePropertyChanged("Height");
            }
        }
        
        
    }
}
