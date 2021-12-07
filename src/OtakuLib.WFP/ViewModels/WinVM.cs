using GihanSoft.AppBase;
using GihanSoft.MangaSources;
using GihanSoft.Navigation.WPF;

using OtakuLib.Logic.Pages;
using OtakuLib.Logic.Services;
using OtakuLib.MangaSourceBase;

namespace OtakuLib.WPF.ViewModels;

public class WinVM : ViewModelBase
{
    private bool isBackStackOpen;
    private bool isForwardStackOpen;

    public WinVM(PageNavigator pageNavigator, IFullScreenProvider fullScreenProvider, IEnumerable<MangaSource> mangaSources)
    {
        ArgumentNullException.ThrowIfNull(pageNavigator);

        const string path = @"D:\Entertainment\Manga\Fights Break Sphere\237";
        LocalPagesProvider pagesProvider = new(path);

        PageNavigator = pageNavigator;
        FullScreenProvider = fullScreenProvider;
        pageNavigator.NavTo<IPgMain>();
        pageNavigator.NavTo<IPgMangaViewer>();
        var page = pageNavigator.CurrentPage as IPgMangaViewer;
        page!.ViewModel.SetLibManga(new Logic.Models.LibManga()
        {
            Manga = mangaSources.First()
                .GetMangasAsync(
                    new PaginationInfo(0, string.Empty, 0),
                    "fight")
                .Result.First()
        }, 0);
        //page!.ViewModel.PagesViewer.ViewModel.PagesProvider = pagesProvider;
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
