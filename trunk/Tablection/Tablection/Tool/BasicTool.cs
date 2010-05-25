using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TablectionSketch.Tool
{
    public class BasicTool : ToolBase
    {
        /// <summary>
        /// 잉크 캔버스의 편집 모드를 변경하는 툴 스타일
        /// </summary>
        private InkCanvasEditingMode _mode = InkCanvasEditingMode.Ink;
        public InkCanvasEditingMode Mode
        {
            get { return _mode; }
            set 
            { 
                _mode = value;
                RaisePropertyChanged("Mode");
            }
        }
        
    }
}
