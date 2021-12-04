using System.ComponentModel;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;

namespace OtakuLib.Logic.ViewModels
{
    public interface IPgMangaViewerVM : INotifyPropertyChanged
    {
        IEnumerable<IPagesViewer> AvailablePagesViewers { get; }
        IPagesViewer PagesViewer { get; set; }
        LibManga? LibManga { get; set; }
    }
}
