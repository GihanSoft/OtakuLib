using System.Globalization;
using System.Windows.Data;

namespace OtakuLib.WPF.XamlConverters;

[ValueConversion(typeof(object), typeof(Type))]
public sealed class ObjectToType : IValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static ObjectToType Default { get; } = new();

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.GetType() ?? value;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
