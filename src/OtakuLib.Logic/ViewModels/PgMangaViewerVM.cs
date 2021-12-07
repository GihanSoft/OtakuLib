using GihanSoft.AppBase;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;
using OtakuLib.Logic.Models.Settings;

namespace OtakuLib.Logic.ViewModels;

internal class PgMangaViewerVM : ViewModelBase, IPgMangaViewerVM
{
    private LibManga libManga;
    private IPagesViewer pagesViewer;
    private int chapter;

    public PgMangaViewerVM(IEnumerable<IPagesViewer> availablePagesViewers, IDataManager<MainSettings> settings)
    {
        var defaultMangaViewerId = settings.Fetch().MangaLibSettings.DefaultMangaViewerId;

        AvailablePagesViewers = availablePagesViewers;
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
            if (value is null) { return; }

            value.ViewModel.CopyState(pagesViewer.ViewModel);
            if (LibManga is not null) { LibManga.PagesViewerId = value.Id; }

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

    public void SetLibManga(LibManga libManga, int chapter)
    {
        ArgumentNullException.ThrowIfNull(libManga);

        this.libManga = libManga;
        Chapter = chapter;

        OnPropertyChanged(nameof(LibManga));
    }
}
