using System.Globalization;
using System.Windows.Data;

namespace OtakuLib.WPF.XamlConverters;

[ValueConversion(typeof(int), typeof(int))]
public sealed class IntPlusPlusConverter : IValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static IntPlusPlusConverter Default { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int i) { return ++i; }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int i) { return --i; }

        return value;
    }
}
