using System.Globalization;
using System.Windows.Data;

namespace OtakuLib.WPF.XamlConverters;

public sealed class ZaribConverter : IMultiValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static ZaribConverter Default { get; } = new ZaribConverter();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values?[0] is double length && values[1] is double zarib)
        {
            return length * zarib;
        }

        return targetType;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return targetTypes;
    }
}