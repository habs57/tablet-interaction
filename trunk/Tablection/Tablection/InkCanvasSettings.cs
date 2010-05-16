using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Ink;
using System.Windows.Controls;

namespace TablectionSketch
{
    public static class InkCanvasSettings
    {
        public static event Action<InkCanvasEditingMode> EditingModeChanged;
        public static event Action<DrawingAttributes> DrawingAttributeChanged;

        private static InkCanvasEditingMode _editingMode;
        public static InkCanvasEditingMode EdtingMode
        {
            get { return _editingMode; }
            set 
            { 
                _editingMode = value;

                if (EditingModeChanged != null)
                {
                    EditingModeChanged(_editingMode);                    
                }
            }
        }

        private static DrawingAttributes _drawingAttributes;
        public static DrawingAttributes DrawingAttributes
        {
            get { return _drawingAttributes; }
            set 
            { 
                _drawingAttributes = value;

                if (DrawingAttributeChanged != null)
                {
                    DrawingAttributeChanged(_drawingAttributes);
                }
            }
        }
    }
}
