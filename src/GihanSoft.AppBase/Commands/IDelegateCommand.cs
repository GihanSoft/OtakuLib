using System.Windows.Input;

namespace GihanSoft.AppBase.Commands;

public interface IDelegateCommand : ICommand
{
#pragma warning disable CA1030 // Use events where appropriate
    void RaiseCanExecuteChanged();
#pragma warning restore CA1030 // Use events where appropriate
}
