using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
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
                return new StrokeTool() { Width = 1, Height = 1, Name = "1.0 pt" };      
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
                return new object[] { 0, 0 };
            }            
        }

        #endregion
    }
}
