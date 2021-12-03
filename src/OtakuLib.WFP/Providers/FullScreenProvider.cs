using System.Windows;
using System.Windows.Input;

using GihanSoft.AppBase;

using OtakuLib.Logic.Services;

using Window = HandyControl.Controls.Window;

namespace OtakuLib.WPF.Providers;

public class FullScreenProvider : IFullScreenProvider
{
    public FullScreenProvider()
    {
        CmdSetFullScreen = new ActionCommand<bool>(value => IsFullScreen = value);
    }

    public bool IsFullScreen
    {
        get => Application.Current.MainWindow is Window win && win.IsFullScreen;

        set
        {
            if (Application.Current.MainWindow is not Window win)
            {
                throw new NotSupportedException();
            }

            win.IsFullScreen = value;
        }
    }

    public ICommand CmdSetFullScreen { get; }
}
