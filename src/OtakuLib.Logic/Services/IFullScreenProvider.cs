using System.Windows.Input;

namespace OtakuLib.Logic.Services;

public interface IFullScreenProvider
{
    bool IsFullScreen { get; set; }

    ICommand CmdSetFullScreen { get; }
}
