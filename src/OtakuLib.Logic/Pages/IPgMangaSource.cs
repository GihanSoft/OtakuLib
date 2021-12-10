using GihanSoft.Navigation.Abstraction;

using OtakuLib.Logic.ViewModels;

namespace OtakuLib.Logic.Pages;

public interface IPgMangaSource : IPage
{
    IPgMangaSourceVM ViewModel { get; }
}