using OmniApp.Common.Logging;
using System.Windows.Input;

namespace OmniApp.UI.Common;

public class RelayCommand<T>(Action<T> execute, Func<T, bool>? canExecute = null) : ICommand
{
    private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        bool result = canExecute == null
                      || (parameter == null && typeof(T) == typeof(object))
                      || (parameter is T arg && canExecute(arg));
        return result;
    }

    public void Execute(object? parameter)
    {
        switch (parameter)
        {
            case T arg:
                _execute(arg);
                break;
            case null when typeof(T) == typeof(object):
                _execute(default!);
                break;
            default:
                Logger.Error(LogClass.OmniUiCommon, $"Parameter type mismatch. Expected {typeof(T).Name}, got {parameter?.GetType().Name ?? "null"}");
                break;
        }
    }
}
