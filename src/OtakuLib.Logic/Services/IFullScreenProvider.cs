using System.ComponentModel;
using System.Windows.Input;

namespace OtakuLib.Logic.Services;

public interface IFullScreenProvider : INotifyPropertyChanged
{
    bool IsFullScreen { get; set; }

    ICommand CmdSetFullScreen { get; }
}