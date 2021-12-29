using System.Windows.Input;
using System.Windows.Threading;

using OtakuLib.Logic.Pages;
using OtakuLib.Logic.ViewModels;

namespace OtakuLib.WPF.Pages;

/// <summary>
/// Interaction logic for PgMangaViewer.xaml
/// </summary>
public partial class PgMangaViewer : IPgMangaViewer
{
    public PgMangaViewer(IPgMangaViewerVM viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        IsVisibleChanged += (_, _) => _ = IsVisibleChangedAsync();
    }

    public IPgMangaViewerVM ViewModel { get; }

    private async Task IsVisibleChangedAsync()
    {
        await Dispatcher.Yield();
        ViewModel.FullScreenProvider.IsFullScreen = false;
        grdToolbar.SetCurrentValue(HeightProperty, 32.0);
    }

    private void ZoomOutBtn_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.PagesViewer.ViewModel.Zoom -= 0.1;
    }

    private void ZoomInBtn_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.PagesViewer.ViewModel.Zoom += 0.1;
    }

    protected override void OnPreviewMouseMove(MouseEventArgs e)
    {
        base.OnPreviewMouseMove(e);
        if (!ViewModel.FullScreenProvider.IsFullScreen)
        {
            return;
        }

        var position = Mouse.GetPosition(this);
        if (position.Y <= 10)
        {
            grdToolbar.SetCurrentValue(HeightProperty, 32.0);
        }
        else if (!grdToolbar.IsMouseOver)
        {
            grdToolbar.SetCurrentValue(HeightProperty, 0.0);
        }
    }
}