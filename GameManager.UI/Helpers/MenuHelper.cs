using System.Windows;
using System.Windows.Controls;
using GameManager.Core.Data;

namespace GameManager.UI.Helpers;

public class Menus
{
    private readonly GenreData _genreData = new();
    private readonly PlatformData _platformData = new();

    public void InitializePlatformsMenu(MenuItem menu, RoutedEventHandler? clickHandler = null)
    {
        List<Platform> platforms = _platformData.Deserialize();

        menu.Items.Clear();

        foreach (Platform platform in platforms)
        {
            MenuItem menuItem = new()
            {
                Header = platform.Name,
                IsCheckable = true
            };
            if (clickHandler != null)
            {
                menuItem.Click += clickHandler;
            }
            menu.Items.Add(menuItem);
        }
    }

    public void InitializeGenresMenu(MenuItem menu, RoutedEventHandler? clickHandler = null)
    {
        List<Genre> genres = _genreData.Deserialize();

        menu.Items.Clear();

        foreach (Genre genre in genres)
        {
            MenuItem menuItem = new()
            {
                Header = genre.Name,
                IsCheckable = true
            };
            if (clickHandler != null)
            {
                menuItem.Click += clickHandler;
            }
            menu.Items.Add(menuItem);
        }
    }
}