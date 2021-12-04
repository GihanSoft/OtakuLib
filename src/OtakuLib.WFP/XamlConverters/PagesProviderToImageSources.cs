using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using OtakuLib.MangaSourceBase;

namespace OtakuLib.WPF.XamlConverters;

[ValueConversion(typeof(PagesProvider), typeof(ObservableCollection<ImageSource>))]
public sealed class PagesProviderToImageSources : IValueConverter
{
    /// <summary> Gets the default instance </summary>
    public static PagesProviderToImageSources Default { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not PagesProvider pagesProvider) { return value; }

        var collection = new ObservableCollection<ImageSource>();
        _ = FillCollectionAsync(pagesProvider, collection);
        return collection;
    }

    private static async Task FillCollectionAsync(
        PagesProvider pagesProvider,
        ObservableCollection<ImageSource> imageSources)
    {
        for (int i = 0; i < pagesProvider.Count; i++)
        {
            await Dispatcher.Yield();
            var mem = pagesProvider[i];
            if (mem is null)
            {
                await pagesProvider.LoadPageAsync(i).ConfigureAwait(true);
                mem = pagesProvider[i]!;
            }

            mem.Position = 0;
            var imageSource = BitmapFrame.Create(mem);
            imageSources.Add(imageSource);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
