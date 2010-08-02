using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Windows.Ink;
using TablectionSketch.Tool;

namespace TablectionSketch.Converter
{
    [ValueConversion(typeof(double[]), typeof(StrokeTool))]
    public class DoubleToStrokeToolConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            string option = parameter as string;
                        
            if (option != null)
            {
                double width = (double)values[0];
                double height = (double)values[1];

                return new StrokeTool() { Width = (double)width, Height = (double)height, Name = string.Format("{0} pt", width.ToString()) };                      
            }
            else
            {
                return new StrokeTool() { Width = DrawingAttributes.MinWidth, Height = DrawingAttributes.MinHeight, Name = string.Format("{0} pt", DrawingAttributes.MinWidth) };      
            }            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            StrokeTool tool = value as StrokeTool;
            if (tool != null)
            {
                return new object[] { tool.Width, tool.Height };
            }
            else
            {
                return new object[] { DrawingAttributes.MinWidth, DrawingAttributes.MinHeight };
            }            
        }

        #endregion
    }
}
