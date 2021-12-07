using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

using GihanSoft.AppBase;
using GihanSoft.AppBase.Commands;

using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels;

[SuppressMessage("Build", "CA1812:never instantiated", Justification = "auto build service.")]
internal class PgMangaSourceVM : ViewModelBase, IPgMangaSourceVM
{
    private int receivedMangaCount;
    private readonly ObservableCollection<Manga> mangas;

    private MangaSource? mangaSource;

    public PgMangaSourceVM()
    {
        mangas = new();
        Mangas = new(mangas);
        CmdFetchNextPage = DelegateCommand.Create(FetchNextPageAsync);
    }

    public MangaSource? MangaSource
    {
        get => mangaSource;
        set
        {
            mangaSource = value;
            receivedMangaCount = 0;
            mangas.Clear();
            _ = FetchNextPageAsync();
        }
    }

    public ReadOnlyObservableCollection<Manga> Mangas { get; }

    public int Page => (receivedMangaCount / 20) - 1;

    public ICommand CmdFetchNextPage { get; }

    public async Task FetchNextPageAsync()
    {
        if (mangaSource is null)
        { return; }

        var lastId = mangas.Count > 0 ? mangas[^1].Id : string.Empty;
        PaginationInfo paginationInfo = new(Page + 1, lastId, receivedMangaCount);
        var newMangas = await mangaSource.GetMangasAsync(paginationInfo).ConfigureAwait(false);
        foreach (var manga in newMangas)
        {
            mangas.Add(manga);
        }
    }
}
