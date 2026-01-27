using System.Globalization;
using System.Windows.Data;

namespace BeyondWPF.BaseApplication.Converters;

/// <summary>
/// Compares an enum value with a parameter and returns true if they are equal.
/// </summary>
public class EnumToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null && parameter == null) return true;
        if (value == null || parameter == null) return false;
        return value.ToString() == parameter.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b && b)
        {
            if (parameter == null) return null!;
            
            Type actualType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            return Enum.Parse(actualType, parameter.ToString()!);
        }
        return Binding.DoNothing;
    }
}
