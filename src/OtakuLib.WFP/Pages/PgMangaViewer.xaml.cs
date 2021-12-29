using System.Windows.Input;

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
        IsVisibleChanged += PgMangaViewer_IsVisibleChanged;
    }

    public IPgMangaViewerVM ViewModel { get; }

    private void PgMangaViewer_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        ViewModel.FullScreenProvider.IsFullScreen = false;
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