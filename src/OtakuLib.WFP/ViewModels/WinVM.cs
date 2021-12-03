using GihanSoft.AppBase;
using GihanSoft.MangaSources;
using GihanSoft.Navigation.WPF;

using OtakuLib.Logic.Pages;
using OtakuLib.Logic.Services;

namespace OtakuLib.WPF.ViewModels;

public class WinVM : ViewModelBase
{
    private bool isBackStackOpen;
    private bool isForwardStackOpen;

    public WinVM(PageNavigator pageNavigator, IFullScreenProvider fullScreenProvider)
    {
        ArgumentNullException.ThrowIfNull(pageNavigator);

        const string path = @"D:\Entertainment\Manga\Fights Break Sphere\237";
        LocalPagesProvider pagesProvider = new(path);

        PageNavigator = pageNavigator;
        FullScreenProvider = fullScreenProvider;
        pageNavigator.NavTo<IPgMain>();
        pageNavigator.NavTo<IPgMangaViewer>();
        var page = pageNavigator.CurrentPage as IPgMangaViewer;
        page!.ViewModel.PagesViewer.ViewModel.PagesProvider = pagesProvider;
    }

    public PageNavigator PageNavigator { get; }
    public IFullScreenProvider FullScreenProvider { get; }

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
