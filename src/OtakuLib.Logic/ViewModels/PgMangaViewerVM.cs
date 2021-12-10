using GihanSoft.AppBase;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;
using OtakuLib.Logic.Models.Settings;
using OtakuLib.Logic.Services;

namespace OtakuLib.Logic.ViewModels;

internal class PgMangaViewerVM : ViewModelBase, IPgMangaViewerVM
{
    private readonly IFullScreenProvider fullScreenProvider;

    private LibManga libManga;
    private IPagesViewer pagesViewer;
    private int chapter;
    private bool showTopBar;

    public PgMangaViewerVM(
        IEnumerable<IPagesViewer> availablePagesViewers,
        IDataManager<MainSettings> settings,
        IFullScreenProvider fullScreenProvider)
    {
        var defaultMangaViewerId = settings.Fetch().MangaLibSettings.DefaultMangaViewerId;

        AvailablePagesViewers = availablePagesViewers;
        this.fullScreenProvider = fullScreenProvider;
        pagesViewer = availablePagesViewers.FirstOrDefault(
            viewer => viewer.Id == defaultMangaViewerId,
            availablePagesViewers.First());
        libManga = LibManga.BlankLibManga;
    }

    public IEnumerable<IPagesViewer> AvailablePagesViewers { get; }

    public IPagesViewer PagesViewer
    {
        get => pagesViewer;
        set
        {
            if (value is null)
            { return; }

            value.ViewModel.CopyState(pagesViewer.ViewModel);
            if (LibManga is not null)
            { LibManga.PagesViewerId = value.Id; }

            pagesViewer = value;
            OnPropertyChanged();
        }
    }

    public LibManga LibManga => libManga;

    public int Chapter
    {
        get => chapter;
        set
        {
            pagesViewer.ViewModel.Page = 0;
            pagesViewer.ViewModel.PagesProvider = libManga.Chapters[value].GetPagesProvider();
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

    public void SetLibManga(LibManga libManga, int chapter)
    {
        ArgumentNullException.ThrowIfNull(libManga);

        this.libManga = libManga;
        Chapter = chapter;

        OnPropertyChanged(nameof(LibManga));
    }
}