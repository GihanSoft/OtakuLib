using System.Windows.Input;

namespace GihanSoft.AppBase;

public class ActionCommand<TInput> : ICommand
{
    private readonly Action<TInput> action;
    private bool canExecute;

    public ActionCommand(Action<TInput> action)
    {
        this.action = action ?? throw new ArgumentNullException(nameof(action));
        canExecute = true;
    }

    public event EventHandler? CanExecuteChanged;

    public void SetCanExecute(bool canExecute)
    {
        this.canExecute = canExecute;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter)
    {
        return canExecute;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not TInput input) { return; }

        action(input);
    }
}

public class ActionCommand : ICommand
{
    private readonly Action action;
    private bool canExecute;

    public ActionCommand(Action action)
    {
        this.action = action ?? throw new ArgumentNullException(nameof(action));
        canExecute = true;
    }

    public event EventHandler? CanExecuteChanged;

    public void SetCanExecute(bool canExecute)
    {
        this.canExecute = canExecute;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter)
    {
        return canExecute;
    }

    public void Execute(object? parameter)
    {
        action();
    }
}
