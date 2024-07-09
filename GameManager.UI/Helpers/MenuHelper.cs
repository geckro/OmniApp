using GameManager.Core.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class Menus
{
    private readonly DeveloperData _developerData = new();
    private readonly GenreData _genreData = new();
    private readonly PlatformData _platformData = new();
    private readonly PublisherData _publisherData = new();
    private readonly SeriesData _seriesData = new();

    private static void InitializeMenu<T>(
        MenuItem menu,
        IEnumerable<T> metadataList,
        RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false
    )
        where T : IMetadata
    {
        menu.Items.Clear();
        foreach (T metadata in metadataList)
        {
            MenuItem menuItem = new() { Header = metadata.Name, Margin = new Thickness(0), IsCheckable = true };

            if (clickHandler != null)
            {
                menuItem.Click += clickHandler;
            }

            if (menuStaysOpen)
            {
                menuItem.StaysOpenOnClick = true;
            }

            menu.Items.Add(menuItem);
        }

        if (menuStaysOpen)
        {
            menu.StaysOpenOnClick = true;
        }
    }

    public void InitializePlatformsMenu(MenuItem menu, RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false)
    {
        InitializeMenu(menu, _platformData.Deserialize(), clickHandler, menuStaysOpen);
    }

    public void InitializeGenresMenu(MenuItem menu, RoutedEventHandler? clickHandler = null, bool menuStaysOpen = false)
    {
        InitializeMenu(menu, _genreData.Deserialize(), clickHandler, menuStaysOpen);
    }

    public void InitializeDevelopersMenu(MenuItem menu, RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false)
    {
        InitializeMenu(menu, _developerData.Deserialize(), clickHandler, menuStaysOpen);
    }

    public void InitializePublishersMenu(MenuItem menu, RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false)
    {
        InitializeMenu(menu, _publisherData.Deserialize(), clickHandler, menuStaysOpen);
    }

    public void InitializeSeriesMenu(MenuItem menu, RoutedEventHandler? clickHandler = null, bool menuStaysOpen = false)
    {
        InitializeMenu(menu, _seriesData.Deserialize(), clickHandler, menuStaysOpen);
    }
}
