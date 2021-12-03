using GihanSoft.AppBase;
using GihanSoft.MangaSources;
using GihanSoft.Navigation.WPF;

using OtakuLib.Logic.Pages;

namespace OtakuLib.WPF.ViewModels;

public class WinVM : ViewModelBase
{
    private bool isBackStackOpen;
    private bool isForwardStackOpen;

    public WinVM(PageNavigator pageNavigator)
    {
        ArgumentNullException.ThrowIfNull(pageNavigator);

        var path = @"D:\Entertainment\Manga\Fights Break Sphere\237";
        LocalPagesProvider pagesProvider = new(path);

        PageNavigator = pageNavigator;
        pageNavigator.NavTo<IPgMain>();
        pageNavigator.NavTo<IPgMangaViewer>();
        pageNavigator.NavTo<IPgMain>();
        pageNavigator.NavTo<IPgMangaViewer>();
        var page = pageNavigator.CurrentPage as IPgMangaViewer;
        page.ViewModel.PagesViewer.ViewModel.PagesProvider = pagesProvider;
    }

    public PageNavigator PageNavigator { get; }

    public bool IsBackStackOpen
    {
        get => isBackStackOpen;
        set
        {
            isBackStackOpen = value;
            OnPropertyChanged();
        }
    }
    public bool IsForwardStackOpen
    {
        get => isForwardStackOpen;
        set
        {
            isForwardStackOpen = value;
            OnPropertyChanged();
        }
    }
}
