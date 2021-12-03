using System.Windows.Input;

using GihanSoft.AppBase;

using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels;

internal class PagesViewerVM : ViewModelBase, IPagesViewerVM
{
    private PagesProvider? pagesProvider;
    private int page;
    private double zoom = 1;
    private double offset;

    public PagesViewerVM()
    {
        CmdMoveToNextPage = new ActionCommand(MoveToNextPage);
        CmdMoveToPreviousPage = new ActionCommand(MoveToPreviousPage);
    }

    public PagesProvider? PagesProvider
    {
        get => pagesProvider;
        set
        {
            pagesProvider = value;
            OnPropertyChanged();
        }
    }

    public int Page
    {
        get => page;
        set
        {
            page = value;
            OnPropertyChanged();
        }
    }

    public double Zoom
    {
        get => zoom;
        set
        {
            zoom = value;
            OnPropertyChanged();
        }
    }

    public double Offset
    {
        get => offset;
        set
        {
            offset = value;
            OnPropertyChanged();
        }
    }

    public ICommand CmdMoveToNextPage { get; }

    public ICommand CmdMoveToPreviousPage { get; }

    private void MoveToNextPage()
    {
        if (pagesProvider is not null && page < pagesProvider.Count - 1)
        {
            Page++;
        }
    }

    private void MoveToPreviousPage()
    {
        if (pagesProvider is not null && page > 0)
        {
            Page--;
        }
    }
}
