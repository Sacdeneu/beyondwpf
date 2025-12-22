using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BeyondWPF.BaseApplication.Converters
{
    public class CornerRadiusToTopRightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CornerRadius cr)
            {
                // Return a new CornerRadius with only the TopRight value preserved
                return new CornerRadius(0, cr.TopRight, 0, 0);
            }
            return new CornerRadius(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
