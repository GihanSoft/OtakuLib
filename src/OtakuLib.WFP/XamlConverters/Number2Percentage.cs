using System.Globalization;
using System.Windows.Data;

namespace OtakuLib.WPF.XamlConverters;

[ValueConversion(typeof(double), typeof(int))]
public sealed class Number2Percentage : IValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static Number2Percentage Default { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double d) { return (int)(d * 100); }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string zoomStr || !int.TryParse(zoomStr, out var zoom))
        {
            return value;
        }

        return zoom / 100.0;
    }
}
