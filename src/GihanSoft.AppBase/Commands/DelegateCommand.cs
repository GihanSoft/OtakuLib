namespace GihanSoft.AppBase.Commands;

public static class DelegateCommand
{
    public static IDelegateCommand Create(Action action) =>
        new SyncDelegateCommand<object>(_ => action(), AlwaysTrue);

    public static IDelegateCommand Create<TInput>(Action<TInput?> action) =>
        new SyncDelegateCommand<TInput>(action, AlwaysTrue);

    public static IDelegateCommand Create(Action action, Func<bool> canExecuteAction) =>
        new SyncDelegateCommand<object>(_ => action(), _ => canExecuteAction());

    public static IDelegateCommand Create<TInput>(Action<TInput?> action, Func<TInput?, bool> canExecuteAction) =>
        new SyncDelegateCommand<TInput>(action, canExecuteAction);

    //=====================================================================================================

    public static IAsyncDelegateCommand Create(Func<Task> action) =>
        new AsyncDelegateCommand<object>(_ => action(), AlwaysTrue);

    public static IAsyncDelegateCommand Create<TInput>(Func<TInput?, Task> action) =>
        new AsyncDelegateCommand<TInput>(action, AlwaysTrue);

    public static IAsyncDelegateCommand Create(Func<Task> action, Func<bool> canExecuteAction) =>
        new AsyncDelegateCommand<object>(_ => action(), _ => canExecuteAction());

    public static IAsyncDelegateCommand Create<TInput>(Func<TInput?, Task> action, Func<TInput?, bool> canExecuteAction) =>
        new AsyncDelegateCommand<TInput>(action, canExecuteAction);

    private static bool AlwaysTrue<TInput>(TInput? _)
    { return true; }

    public static bool TryCastValue<TInput>(object? parameter, out TInput? input)
    {
        switch (parameter)
        {
            case TInput value:
                input = value;
                return true;

            case null:
                input = default;
                return true;

            default:
                input = default;
                return false;
        }
    }
}