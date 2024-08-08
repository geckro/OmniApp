using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;

namespace OmniApp.UI.Common.Helpers;

/// <summary>
///     Provides helper methods for showing windows for every application.
/// </summary>
public class WindowHelper
{
    private readonly IServiceProvider _sp;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WindowHelper" /> class.
    /// </summary>
    /// <param name="sp">The service provider.</param>
    public WindowHelper(IServiceProvider sp)
    {
        _sp = sp;
    }

    /// <summary>
    ///     Shows a window of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of window to show. Derived from <see cref="Window" />.</typeparam>
    /// <param name="initAction">An optional action to perform initialization on the window instance before showing it.</param>
    /// <returns>The shown window instance.</returns>
    public T ShowWindow<T>(Action<T>? initAction = null) where T : Window
    {
        Logger.Debug(LogClass.OmniUiCommonHelpers, $"ShowWindow<{typeof(T).Name}> called");
        T window = _sp.GetRequiredService<T>();
        initAction?.Invoke(window);
        window.Show();
        return window;
    }

    /// <summary>
    ///     Shows a dialog window of the specified type.
    ///     A dialog window is the same as a normal window, but the user has to explicitly interact with
    ///     it first before modifying the base window.
    /// </summary>
    /// <typeparam name="T">The type of window to show. Derived from <see cref="Window" />.</typeparam>
    /// <param name="initAction">An optional action to perform initialization on the dialog window instance before showing it.</param>
    /// <returns>The shown dialog window instance.</returns>
    public T ShowDialogWindow<T>(Action<T>? initAction = null) where T : Window
    {
        Logger.Debug(LogClass.OmniUiCommonHelpers, $"ShowDialogWindow<{typeof(T).Name}> called");
        T dialogWindow = _sp.GetRequiredService<T>();
        initAction?.Invoke(dialogWindow);
        dialogWindow.ShowDialog();
        return dialogWindow;
    }
}
