using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels;

public interface IPgMangaSourceVM : INotifyPropertyChanged
{
    public MangaSource? MangaSource { get; set; }
    public ReadOnlyObservableCollection<Manga> Mangas { get; }
    public int Page { get; }

    public ICommand CmdFetchNextPage { get; }
}
