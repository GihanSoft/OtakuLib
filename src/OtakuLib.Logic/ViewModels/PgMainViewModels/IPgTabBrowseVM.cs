using System.ComponentModel;
using System.Windows.Input;

using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels.PgMainViewModels;

public interface IPgTabBrowseVM : INotifyPropertyChanged
{
    IEnumerable<MangaSource> MangaSources { get; }

    ICommand CmdOpenSource { get; }
}
