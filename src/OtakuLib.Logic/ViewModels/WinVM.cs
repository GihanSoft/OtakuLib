using System.ComponentModel;

using GihanSoft.ApplicationFrameworkBase;

namespace OtakuLib.Logic.ViewModels
{
    internal sealed class WinVM : ViewModelBase, IWinVM
    {
        private string title = "OtakuLib";

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
    }

    public interface IWinVM : INotifyPropertyChanged
    {
        string Title { get; set; }
    }
}
