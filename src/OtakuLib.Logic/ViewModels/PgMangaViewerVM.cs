using GihanSoft.AppBase;

using OtakuLib.Logic.Components;
using OtakuLib.Logic.Models;

namespace OtakuLib.Logic.ViewModels
{
    public class PgMangaViewerVM : ViewModelBase, IPgMangaViewerVM
    {
        private LibManga? libManga;
        private IPagesViewer pagesViewer;

        public PgMangaViewerVM(IEnumerable<IPagesViewer> availablePagesViewers)
        {
            AvailablePagesViewers = availablePagesViewers;
            pagesViewer = availablePagesViewers.First();
        }

        public IEnumerable<IPagesViewer> AvailablePagesViewers { get; }

        public IPagesViewer PagesViewer
        {
            get => pagesViewer;
            set
            {
                if (value is null) { return; }

                value.ViewModel.PagesProvider = pagesViewer.ViewModel.PagesProvider;
                value.ViewModel.Page = pagesViewer.ViewModel.Page;
                value.ViewModel.Zoom = pagesViewer.ViewModel.Zoom;
                value.ViewModel.Offset = pagesViewer.ViewModel.Offset;

                pagesViewer = value;
                OnPropertyChanged();
            }
        }

        public LibManga? LibManga
        {
            get => libManga;
            set
            {
                libManga = value;
                OnPropertyChanged();
            }
        }
    }
}
