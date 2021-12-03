using System.ComponentModel;

namespace OtakuLib.Logic.ViewModels.PgMainViewModels;

public interface IPgMainVM : INotifyPropertyChanged
{
    IPgTabBrowseVM TabBrowseVM { get; }
}

