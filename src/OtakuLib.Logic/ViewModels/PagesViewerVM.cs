using System.Windows.Input;

using GihanSoft.AppBase;
using GihanSoft.AppBase.Commands;

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
        CmdSetPage = DelegateCommand.Create((int? value) => Page = value ?? 1, value => value.HasValue);
        CmdSetZoom = DelegateCommand.Create((double? value) => Zoom = value ?? 100, value => value.HasValue);
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
            if (pagesProvider is null || value < 0 || value > pagesProvider.Count - 1)
            {
                return;
            }

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

    public ICommand CmdSetPage { get; }
    public ICommand CmdSetZoom { get; }

    public void CopyStateFrom(IPagesViewerVM pagesViewer)
    {
        PagesProvider = pagesViewer.PagesProvider;
        Page = pagesViewer.Page;
        Zoom = pagesViewer.Zoom;
        Offset = pagesViewer.Offset;
    }
}