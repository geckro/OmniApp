using System.Windows;
using System.Windows.Controls;
using GameManager.Core.Data;

namespace GameManager.UI.Helpers;

public class Menus
{
    private readonly GenreData _genreData = new();
    private readonly PlatformData _platformData = new();

    private static void InitializeMenu<T>(
        MenuItem menu,
        IEnumerable<T> metadataList,
        RoutedEventHandler? clickHandler = null
    )
        where T : IMetadata
    {
        menu.Items.Clear();
        foreach (T metadata in metadataList)
        {
            MenuItem menuItem = new()
            {
                Header = metadata.Name,
                IsCheckable = true
            };
            if (clickHandler != null)
            {
                menuItem.Click += clickHandler;
            }

            menu.Items.Add(menuItem);
        }
    }

    public void InitializePlatformsMenu(MenuItem menu, RoutedEventHandler? clickHandler = null)
    {
        InitializeMenu(menu, _platformData.Deserialize(), clickHandler);
    }

    public void InitializeGenresMenu(MenuItem menu, RoutedEventHandler? clickHandler = null)
    {
        InitializeMenu(menu, _genreData.Deserialize(), clickHandler);
    }
}