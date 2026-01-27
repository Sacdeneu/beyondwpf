using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BeyondWPF.BaseApplication.Converters;

/// <summary>
/// Converts a string to Visibility. Visible if not null or empty, Collapsed otherwise.
/// </summary>
public class StringVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !string.IsNullOrEmpty(value as string) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
