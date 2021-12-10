using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;

using OtakuLib.Logic.Pages;
using OtakuLib.Logic.ViewModels;

namespace OtakuLib.WPF.Pages;

/// <summary>
/// Interaction logic for PgMangaSource.xaml
/// </summary>
[SuppressMessage("Build", "CA1812:never instantiated", Justification = "auto build service.")]
internal partial class PgMangaSource : IPgMangaSource
{
    public PgMangaSource(IPgMangaSourceVM viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        Binding titleBinding = new()
        {
            Source = viewModel,
            Path = new PropertyPath("MangaSource.Name"),
        };
        SetBinding(TitleProperty, titleBinding);
    }

    public IPgMangaSourceVM ViewModel { get; }
}