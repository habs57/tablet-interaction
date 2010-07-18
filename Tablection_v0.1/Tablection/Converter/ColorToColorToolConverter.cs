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
    [ValueConversion(typeof(Color), typeof(ColorTool))]
    public class ColorToColorToolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color = (Color)value;
            return new ColorTool() { Color = new SolidColorBrush(color), Name = color.ToString() };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ColorTool tool = value as ColorTool;
            if (tool != null)
            {
                SolidColorBrush brush = tool.Color as SolidColorBrush;
                if (brush != null)
                {
                    return brush.Color;
                }
            }

            return Colors.Black;
        }

        #endregion
    }
}
