using System.ComponentModel;
using System.Windows.Input;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;
using OtakuLib.Logic.Services;

namespace OtakuLib.Logic.ViewModels;

public interface IPgMangaViewerVM : INotifyPropertyChanged
{
    IFullScreenProvider FullScreenProvider { get; }

    IEnumerable<IPagesViewer> AvailablePagesViewers { get; }
    IPagesViewer PagesViewer { get; set; }

    LibManga LibManga { get; }

    int Chapter { get; set; }
    bool ShowTopBar { get; set; }

    ICommand CmdMoveToChapter { get; set; }

    public void SetLibManga(LibManga libManga, int chapter);
}