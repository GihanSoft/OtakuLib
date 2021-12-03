using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

using OtakuLib.MangaSourceBase;

namespace OtakuLib.WPF.XamlConverters;

public sealed class StreamToImageSource : IMultiValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static StreamToImageSource Default { get; } = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is null || values[0] is not PagesProvider pagesProvider || values[1] is not int page)
        {
            return targetType;
        }

        var mem = pagesProvider.GetPage(page);
        if (mem is null)
        {
            pagesProvider.LoadPageAsync(page).GetAwaiter().GetResult();
            mem = pagesProvider.GetPage(page);
        }

        mem.Position = 0;
        return BitmapFrame.Create(mem);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return targetTypes.ToArray();
    }
}
