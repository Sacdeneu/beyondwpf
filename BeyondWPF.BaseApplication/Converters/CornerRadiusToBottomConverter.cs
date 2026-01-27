using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BeyondWPF.BaseApplication.Converters
{
    /// <summary>
    /// Converts a CornerRadius to a new one with only bottom corners.
    /// </summary>
    public class CornerRadiusToBottomConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CornerRadius cr)
            {
                return new CornerRadius(0, 0, cr.BottomRight, cr.BottomLeft);
            }
            return new CornerRadius(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
