using OtakuLib.Logic.ViewModels;

namespace OtakuLib.Logic.Components;

public interface IPagesViewer
{
    string Id { get; }
    string Title { get; }
    IPagesViewerVM ViewModel { get; }
}