using System.Globalization;
using System.Windows.Data;

namespace OtakuLib.WPF.XamlConverters;

[ValueConversion(typeof(int), typeof(int))]
public sealed class IntSumConverter : IValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static IntSumConverter Default { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int x && parameter is string yStr && int.TryParse(yStr, out var y))
        {
            return x + y;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string xStr && int.TryParse(xStr, out var x) && parameter is string yStr && int.TryParse(yStr, out var y))
        {
            return x - y;
        }

        return value;
    }
}