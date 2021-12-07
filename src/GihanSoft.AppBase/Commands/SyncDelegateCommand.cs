namespace GihanSoft.AppBase.Commands;

internal sealed class SyncDelegateCommand<TInput> : IDelegateCommand
{
    private readonly Action<TInput?> execute;

    private readonly Func<TInput?, bool> canExecute;

    public event EventHandler? CanExecuteChanged;

    public SyncDelegateCommand(Action<TInput?> execute, Func<TInput?, bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        ArgumentNullException.ThrowIfNull(canExecute);

        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return DelegateCommand.TryCastValue<TInput>(parameter, out var input) && canExecute(input);
    }

    public void Execute(object? parameter)
    {
        if (DelegateCommand.TryCastValue<TInput>(parameter, out var input) && canExecute(input))
        {
            execute(input);
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}