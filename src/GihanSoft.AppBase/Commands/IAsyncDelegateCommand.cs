namespace GihanSoft.AppBase.Commands;

public interface IAsyncDelegateCommand : IDelegateCommand
{
    Task ExecuteAsync(object? parameter);
}
