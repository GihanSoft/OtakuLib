using System.ComponentModel;

using GihanSoft.AppBase;

using OtakuLib.Logic.Components;

namespace OtakuLib.Logic.ViewModels
{
    public class PgMangaViewerVM : ViewModelBase, IPgMangaViewerVM
    {
        public PgMangaViewerVM(IEnumerable<IPagesViewer> availablePagesViewers)
        {
            AvailablePagesViewers = availablePagesViewers;
            PagesViewer = availablePagesViewers.First();
        }

        public IEnumerable<IPagesViewer> AvailablePagesViewers { get; }

        public IPagesViewer PagesViewer { get; set; }
    }
}
