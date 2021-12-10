using System.Windows.Input;

using GihanSoft.AppBase.Commands;

using OtakuLib.Logic.Services;

using Window = HandyControl.Controls.Window;

namespace OtakuLib.WPF.Providers;

public class FullScreenProvider : IFullScreenProvider
{
    public FullScreenProvider()
    {
        CmdSetFullScreen = DelegateCommand.Create((bool? value) => IsFullScreen = value ?? false);
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