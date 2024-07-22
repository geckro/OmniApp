using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public interface IWindowHelper
{
    void LoadContent(ContentControl contentControl, Window window);
    void ShowWindow<T>() where T : Window;
}

public class WindowHelper(IServiceProvider serviceProvider) : IWindowHelper
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    /// <summary>
    ///     Binds a ContentControl to a window.
    /// </summary>
    /// <param name="contentControl">The ContentControl to bind.</param>
    /// <param name="window">The Window to bind to</param>
    public void LoadContent(ContentControl contentControl, Window window)
    {
        ArgumentNullException.ThrowIfNull(contentControl);
        ArgumentNullException.ThrowIfNull(window);

        contentControl.Content = window.Content;
    }

    /// <summary>
    ///     Shows the window.
    /// </summary>
    public void ShowWindow<T>() where T : Window
    {
        Logger.Debug(LogClass.GameMgrUi, $"ShowWindow<{typeof(T).Name}> called");
        T window = _serviceProvider.GetRequiredService<T>();
        window.Show();
    }
}
