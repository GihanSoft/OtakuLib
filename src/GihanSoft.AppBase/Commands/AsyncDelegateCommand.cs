namespace GihanSoft.AppBase.Commands;

internal class AsyncDelegateCommand<TInput> : IAsyncDelegateCommand
{
    private readonly Func<TInput?, Task> execute;
    private readonly Func<TInput?, bool> canExecute;

    private bool canExecuteManual;

    public AsyncDelegateCommand(Func<TInput?, Task> execute, Func<TInput?, bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        ArgumentNullException.ThrowIfNull(canExecute);

        this.execute = execute;
        this.canExecute = canExecute;

        canExecuteManual = true;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return DelegateCommand.TryCastValue<TInput>(parameter, out var input) && canExecute(input);
    }

    public void Execute(object? parameter)
    {
        _ = ExecuteAsync(parameter);
    }

    public async Task ExecuteAsync(object? parameter)
    {
        if (canExecuteManual && DelegateCommand.TryCastValue<TInput>(parameter, out var input) && canExecute(input))
        {
            SetCanExecuteManual(false);
            await execute(input).ConfigureAwait(true);
            SetCanExecuteManual(true);
        }
    }

    public void SetCanExecuteManual(bool value)
    {
        canExecuteManual = value;
        RaiseCanExecuteChanged();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
