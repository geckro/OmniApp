using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public static class WindowHelper
{
    public static void LoadContent(ContentControl contentControl, Window window)
    {
        contentControl.Content = window.Content;
    }

    public static void ShowWindow(Window window)
    {
        window.Show();
    }
}
