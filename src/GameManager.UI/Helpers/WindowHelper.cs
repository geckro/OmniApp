using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public interface IWindowHelper
{
    void LoadContent(ContentControl contentControl, Window window);
    void ShowWindow<T>() where T : Window, new();
    void ShowWindow(Window? window);
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
    public void ShowWindow<T>() where T : Window, new()
    {
        T? window = (T?)_serviceProvider.GetService(typeof(T));
        if (window == null)
        {
            throw new InvalidOperationException($"Unable to create an instance of {typeof(T).Name}. Ensure it is registered with the dependency injection container.");
        }
        ShowWindow(window);
    }

    /// <summary>
    ///     Shows the specified window.
    /// </summary>
    /// <param name="window">The window to show.</param>
    public void ShowWindow(Window? window)
    {
        ArgumentNullException.ThrowIfNull(window);
        window.Show();
    }
}
