using System.ComponentModel;
using System.Windows.Input;

using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels;

public interface IPagesViewerVM : INotifyPropertyChanged
{
    PagesProvider? PagesProvider { get; set; }
    int Page { get; set; }
    double Zoom { get; set; }
    double Offset { get; set; }

    ICommand CmdMoveToNextPage { get; }
    ICommand CmdMoveToPreviousPage { get; }

    public void CopyState(IPagesViewerVM pagesViewer);
}