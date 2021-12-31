using GihanSoft.AppBase;
using GihanSoft.Navigation.WPF;

using OtakuLib.Logic.Pages;
using OtakuLib.MangaSourceBase;

namespace OtakuLib.WPF.ViewModels;

public class WinVM : ViewModelBase
{
    private bool isBackStackOpen;
    private bool isForwardStackOpen;

    public WinVM(PageNavigator pageNavigator, IEnumerable<MangaSource> mangaSources)
    {
        ArgumentNullException.ThrowIfNull(pageNavigator);

        PageNavigator = pageNavigator;
        pageNavigator.NavTo<IPgMain>();
        pageNavigator.NavTo<IPgMangaViewer>();
        var page = pageNavigator.CurrentPage as IPgMangaViewer;
        page!.ViewModel.SetLibManga(new Logic.Models.LibManga()
        {
            Manga = mangaSources.First()
                .GetMangasAsync(
                    new PaginationInfo(0, string.Empty, 0),
                    "feng")
                .Result.First()
        }, 0);
        //page!.ViewModel.PagesViewer.ViewModel.PagesProvider = pagesProvider;
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