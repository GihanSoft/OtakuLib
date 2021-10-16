using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GihanSoft.ApplicationFrameworkBase
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) { throw new ArgumentNullException(nameof(propertyName)); }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
