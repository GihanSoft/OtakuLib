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
    }

    public IPgMangaViewerVM ViewModel { get; }

    private void ZoomOutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ViewModel.PagesViewer.ViewModel.Zoom -= 0.1;
    }

    private void ZoomInBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ViewModel.PagesViewer.ViewModel.Zoom += 0.1;
    }
}