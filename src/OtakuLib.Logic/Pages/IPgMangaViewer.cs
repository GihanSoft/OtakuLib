using GihanSoft.Navigation.Abstraction;

using OtakuLib.Logic.ViewModels;

namespace OtakuLib.Logic.Pages;

public interface IPgMangaViewer : IPage
{
    IPgMangaViewerVM ViewModel { get; }
}