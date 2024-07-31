using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;

namespace OmniApp.UiCommon.Helpers;

public class WindowHelper(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    /// <summary>
    ///     Shows the window.
    /// </summary>
    public T ShowWindow<T>(Action<T>? initAction = null) where T : Window
    {
        Logger.Debug(LogClass.GameMgrUi, $"ShowWindow<{typeof(T).Name}> called");
        T window = _serviceProvider.GetRequiredService<T>();
        initAction?.Invoke(window);
        window.Show();
        return window;
    }

    /// <summary>
    ///     Shows the dialog window.
    /// </summary>
    public T ShowDialogWindow<T>(Action<T>? initAction = null) where T : Window
    {
        Logger.Debug(LogClass.GameMgrUi, $"ShowDialogWindow<{typeof(T).Name}> called");
        T dialogWindow = _serviceProvider.GetRequiredService<T>();
        initAction?.Invoke(dialogWindow);
        dialogWindow.ShowDialog();
        return dialogWindow;
    }
}
