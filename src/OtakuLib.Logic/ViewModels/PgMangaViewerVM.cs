using System.Windows.Input;

using GihanSoft.AppBase;
using GihanSoft.AppBase.Commands;
using GihanSoft.AppBase.Services;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;
using OtakuLib.Logic.Models.Settings;
using OtakuLib.Logic.Services;

namespace OtakuLib.Logic.ViewModels;

internal class PgMangaViewerVM : ViewModelBase, IPgMangaViewerVM
{
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
        FullScreenProvider = fullScreenProvider;
        pagesViewer = availablePagesViewers.FirstOrDefault(
            viewer => viewer.Id == defaultMangaViewerId,
            availablePagesViewers.First());

        libManga = LibManga.BlankLibManga;

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

    public LibManga LibManga => libManga;

    public int Chapter
    {
        get => chapter;
        set
        {
            if (value < 0 || value >= libManga.Chapters.Count)
            {
                return;
            }

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

    public ICommand CmdMoveToChapter { get; set; }

    public void SetLibManga(LibManga libManga, int chapter)
    {
        ArgumentNullException.ThrowIfNull(libManga);

        this.libManga = libManga;
        Chapter = chapter;

        OnPropertyChanged(nameof(LibManga));
    }
}