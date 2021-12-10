using OtakuLib.Logic.Models;

namespace OtakuLib.Logic.ViewModels.PgMainViewModels;

public interface IPgTabLibraryVM
{
    public IEnumerable<LibManga> LibMangas { get; }
}