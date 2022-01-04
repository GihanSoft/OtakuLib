using System.Globalization;
using System.Windows.Data;

namespace OtakuLib.WPF.XamlConverters;

[ValueConversion(typeof(double), typeof(double))]
public sealed class DoubleSumConverter : IValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static DoubleSumConverter Default { get; } = new DoubleSumConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double x && parameter is string yStr && double.TryParse(yStr, out var y))
        {
            return x + y;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string xStr && double.TryParse(xStr, out var x) && parameter is string yStr && double.TryParse(yStr, out var y))
        {
            return x - y;
        }

        return value;
    }
}
