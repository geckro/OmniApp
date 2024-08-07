using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;

namespace OmniApp.UI.Common.Helpers;

public class WindowHelper
{
    private readonly IServiceProvider _sp;

    public WindowHelper(IServiceProvider sp)
    {
        _sp = sp;
    }

    /// <summary>
    ///     Shows the window.
    /// </summary>
    public T ShowWindow<T>(Action<T>? initAction = null) where T : Window
    {
        Logger.Debug(LogClass.OmniUiCommonHelpers, $"ShowWindow<{typeof(T).Name}> called");
        T window = _sp.GetRequiredService<T>();
        initAction?.Invoke(window);
        window.Show();
        return window;
    }

    /// <summary>
    ///     Shows the dialog window.
    /// </summary>
    public T ShowDialogWindow<T>(Action<T>? initAction = null) where T : Window
    {
        Logger.Debug(LogClass.OmniUiCommonHelpers, $"ShowDialogWindow<{typeof(T).Name}> called");
        T dialogWindow = _sp.GetRequiredService<T>();
        initAction?.Invoke(dialogWindow);
        dialogWindow.ShowDialog();
        return dialogWindow;
    }
}
