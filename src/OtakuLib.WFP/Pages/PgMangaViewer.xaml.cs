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
}
