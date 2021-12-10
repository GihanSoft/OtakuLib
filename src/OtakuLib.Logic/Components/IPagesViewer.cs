using OtakuLib.Logic.ViewModels;

namespace OtakuLib.Logic.Components;

public interface IPagesViewer
{
    string Id { get; }
    IPagesViewerVM ViewModel { get; }
    string Title { get; }
}