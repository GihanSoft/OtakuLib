using System.ComponentModel;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;
using OtakuLib.MangaSourceBase;

namespace OtakuLib.Logic.ViewModels
{
    public interface IPgMangaViewerVM : INotifyPropertyChanged
    {
        IEnumerable<IPagesViewer> AvailablePagesViewers { get; }
        IPagesViewer PagesViewer { get; set; }
    }
}
