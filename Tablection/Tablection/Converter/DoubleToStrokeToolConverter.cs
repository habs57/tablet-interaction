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
    [ValueConversion(typeof(double), typeof(StrokeTool))]
    public class DoubleToStrokeToolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double v = (double)value;
            string option = parameter as string;
            if (option != null)
            {
                if (option.Equals("Width") == true)
                {
                    return new StrokeTool() { Width = v, Name = v.ToString() };
                }
                else if (option.Equals("Height") == true)
                {
                    return new StrokeTool() { Width = v, Name = v.ToString() };
                }
            }
           
            return new StrokeTool() { Width = v, Height = v, Name = v.ToString() };            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StrokeTool tool = value as StrokeTool;
            if (tool != null)
            {
                string option = parameter as string;
                if (option != null)
                {
                    if (option.Equals("Width") == true)
                    {
                        return tool.Width;
                    }
                    else if (option.Equals("Height") == true)
                    {
                        return tool.Height;
                    }                    
                }

                return Math.Max(tool.Width, tool.Height);
            }

            return 1;
        }

        #endregion
    }
}
