using System.Windows.Input;
using System.Windows.Shell;

using GihanSoft.AppBase;
using GihanSoft.AppBase.Commands;

using OtakuLib.Logic.Services;

using Window = HandyControl.Controls.Window;

namespace OtakuLib.WPF.Providers;

public class FullScreenProvider : ViewModelBase, IFullScreenProvider
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
            var chrome = WindowChrome.GetWindowChrome(win);
            chrome.ResizeBorderThickness = new Thickness(value ? 0 : 8);

            NotifyPropertyChanged();
        }
    }

    public ICommand CmdSetFullScreen { get; }
}