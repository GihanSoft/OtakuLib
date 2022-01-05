using System.Windows.Input;

using GihanSoft.AppBase;
using GihanSoft.AppBase.Commands;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;
using OtakuLib.Logic.Models.Settings;
using OtakuLib.Logic.Services;
using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels;

internal class PgMangaViewerVM : ViewModelBase, IPgMangaViewerVM
{
    private readonly IEnumerable<MangaSource> mangaSources;

    private IPagesViewer pagesViewer;
    private int chapter;
    private bool showTopBar;

    private IEnumerable<Chapter> chapters;

    public PgMangaViewerVM(
        IEnumerable<MangaSource> mangaSources,
        IEnumerable<IPagesViewer> availablePagesViewers,
        IDataManager<AppData> settings,
        IFullScreenProvider fullScreenProvider)
    {
        const string? defaultMangaViewerId = "GihanSoft.SinglePage";

        this.mangaSources = mangaSources;

        AvailablePagesViewers = availablePagesViewers;
        FullScreenProvider = fullScreenProvider;
        pagesViewer = availablePagesViewers.FirstOrDefault(
            viewer => viewer.Id == defaultMangaViewerId,
            availablePagesViewers.First());

        chapters = Enumerable.Empty<Chapter>();

        LibManga = new();

        CmdMoveToChapter = DelegateCommand.Create(
            chapter => Chapter = chapter ?? -1,
            static (int? chapter) => chapter.HasValue);
    }

    public IFullScreenProvider FullScreenProvider { get; }
    public IEnumerable<IPagesViewer> AvailablePagesViewers { get; }

    public IPagesViewer PagesViewer
    {
        get => pagesViewer;
        set
        {
            if (value is null)
            { return; }

            value.ViewModel.CopyStateFrom(pagesViewer.ViewModel);
            if (LibManga is not null)
            { LibManga.PagesViewerId = value.Id; }

            pagesViewer = value;
            OnPropertyChanged();
        }
    }

    public LibManga LibManga { get; private set; }

    public int Chapter
    {
        get => chapter;
        set
        {
            if (value < 0 || value >= LibManga.Chapters.Count)
            {
                return;
            }

            pagesViewer.ViewModel.PagesProvider = chapters.First(c => c.Id == LibManga.Chapters[value].Id)
                .GetPagesProviderAsync().Result;
            pagesViewer.ViewModel.Page = 0;
            pagesViewer.ViewModel.Offset = 0;

            chapter = value;
            OnPropertyChanged();
        }
    }

    public bool ShowTopBar
    {
        get => showTopBar;
        set
        {
            showTopBar = value;
            OnPropertyChanged();
        }
    }

    public ICommand CmdMoveToChapter { get; set; }

    public async Task SetLibMangaAsync(LibManga libManga, int chapterIndex)
    {
        ArgumentNullException.ThrowIfNull(libManga);
        if (libManga.Id is null)
        {
            throw new ArgumentException("Manga id is null", nameof(libManga));
        }

        if (libManga.Chapters.Count == 0)
        {
            var manga = await mangaSources.First(s => s.Id == libManga.SourceId)
                .GetMangaAsync(libManga.Id)
                .ConfigureAwait(false);

            chapters = await manga.GetChaptersAsync().ConfigureAwait(false);
            foreach (var chapter in chapters)
            {
                libManga.Chapters.Add(new LibMangaChapter(chapter.Id, chapter.Title, 0));
            }
        }

        LibManga = libManga;
        OnPropertyChanged(nameof(LibManga));
        Chapter = chapterIndex;
    }
}