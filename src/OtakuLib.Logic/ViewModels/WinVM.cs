using System.ComponentModel;

using GihanSoft.ApplicationFrameworkBase;

using OtakuLib.Logic.Services;
using OtakuLib.Logic.Utilities;

namespace OtakuLib.Logic.ViewModels
{
    internal sealed class WinVM : ViewModelBase, IWinVM
    {
        private string title = "OtakuLib";
        private int windowState = 2;
        private double top = 50;
        private double left = 50;
        private double height = 450;
        private double width = 800;

        public WinVM(ISettingsManager settingsManager)
        {
            var mainSettings = settingsManager.GetMainSettings();

            windowState = mainSettings.AppearanceSettings.WindowState;
            top = mainSettings.AppearanceSettings.Top;
            left = mainSettings.AppearanceSettings.Left;
            height = mainSettings.AppearanceSettings.Height;
            width = mainSettings.AppearanceSettings.Width;
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public int WindowState
        {
            get => windowState;
            set
            {
                windowState = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }

        public double Top
        {
            get => top;
            set
            {
                top = value;
                OnPropertyChanged();
            }
        }

        public double Left
        {
            get => left;
            set
            {
                left = value;
                OnPropertyChanged();
            }
        }
    }

    public interface IWinVM : INotifyPropertyChanged
    {
        string Title { get; set; }
        int WindowState { get; set; }
        double Height { get; set; }
        double Width { get; set; }
        double Top { get; set; }
        double Left { get; set; }
    }
}
