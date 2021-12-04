using OtakuLib.Logic.ViewModels;

namespace OtakuLib.Logic.Components;

public interface IPagesViewer
{
    IPagesViewerVM ViewModel { get; }
    string Title { get; }
}
