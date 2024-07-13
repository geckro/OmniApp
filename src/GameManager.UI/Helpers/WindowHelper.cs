using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

/// <summary>
///     Helper for various window related functions.
/// </summary>
public static class WindowHelper
{
    /// <summary>
    ///     Binds a ContentControl to a window.
    /// </summary>
    /// <param name="contentControl">The ContentControl to bind.</param>
    /// <param name="window">The Window to bind to</param>
    public static void LoadContent(ContentControl contentControl, Window window)
    {
        contentControl.Content = window.Content;
    }

    /// <summary>
    ///     Shows the window.
    /// </summary>
    /// <param name="window">The window to show.</param>
    public static void ShowWindow(Window window)
    {
        window.Show();
    }
}
