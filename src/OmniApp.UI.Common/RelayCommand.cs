using OmniApp.Common.Logging;
using System.Windows.Input;

namespace OmniApp.UI.Common;

/// <summary>
///     Represents a command that can be executed in an <see cref="ICommand" />-compatible framework,
///     and conveniently used to create commands.
/// </summary>
/// <typeparam name="T">The type of the parameter that the <see cref="ICommand" /> can handle.</typeparam>
public class RelayCommand<T> : ICommand
{
    private readonly Func<T, bool>? _canExecute;
    private readonly Action<T> _execute;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RelayCommand{T}" /> class.
    /// </summary>
    /// <param name="execute">The delegate to invoke when the command is executed.</param>
    /// <param name="canExecute">The delegate to invoke to see if the command can be executed.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="canExecute" /> is <c>null</c>.</exception>
    public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
    {
        _canExecute = canExecute;
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    /// <summary>
    ///     Occurs when the ability to execute the command has changed.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    ///     Determines if the command can be executed.
    /// </summary>
    /// <param name="parameter">The parameter to be passed to the command.</param>
    /// <returns><c>true</c> if the command can be executed, <c>false</c> if not.</returns>
    public bool CanExecute(object? parameter)
    {
        bool result = _canExecute == null || (parameter == null && typeof(T) == typeof(object)) ||
                      (parameter is T arg && _canExecute(arg));
        return result;
    }

    /// <summary>
    ///     Executes the command.
    /// </summary>
    /// <param name="parameter">The parameter to be passed to the command.</param>
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
                Logger.Error(LogClass.OmniUiCommonRelayCommand,
                        $"Parameter type mismatch. Expected {typeof(T).Name}, got {parameter?.GetType().Name ?? "null"}");
                break;
        }
    }
}
