using GihanSoft.AppBase;

namespace OtakuLib.Logic.ViewModels.PgMainViewModels;

public class PgMainVM : ViewModelBase, IPgMainVM
{
    public PgMainVM(IPgTabBrowseVM tabBrowseVM)
    {
        TabBrowseVM = tabBrowseVM;
    }

    public IPgTabBrowseVM TabBrowseVM { get; }
}
