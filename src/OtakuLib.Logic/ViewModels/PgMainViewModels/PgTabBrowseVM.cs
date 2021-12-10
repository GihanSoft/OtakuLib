using System.Windows.Input;

using GihanSoft.AppBase;
using GihanSoft.AppBase.Commands;
using GihanSoft.Navigation.Abstraction;

using OtakuLib.Logic.Pages;
using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels.PgMainViewModels;

public class PgTabBrowseVM : ViewModelBase, IPgTabBrowseVM
{
    private readonly IPageNavigator pageNavigator;

    public PgTabBrowseVM(
        IPageNavigator pageNavigator,
        IEnumerable<MangaSource> mangaSources)
    {
        this.pageNavigator = pageNavigator;
        MangaSources = mangaSources;

        CmdOpenSource = DelegateCommand.Create(OpenSource, (MangaSource? input) => input is not null);
    }

    public IEnumerable<MangaSource> MangaSources { get; }

    public ICommand CmdOpenSource { get; }

    private void OpenSource(MangaSource mangaSource)
    {
        pageNavigator.NavTo<IPgMangaSource>();

        if (pageNavigator.CurrentPage is IPgMangaSource pg)
        {
            pg.ViewModel.MangaSource = mangaSource;
        }
    }
}