using GameManager.UI.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class Import
{
    private readonly Menus _menuHelper = new();

    public Import()
    {
        InitializeComponent();

        ImportGameDataGrid.ColumnWidth = new DataGridLength(100);
        ImportGameDataGrid.MinColumnWidth = 50;

        PopulateDataGrid();
    }

    private void PopulateDataGrid()
    {
    }

    private static MenuItem GetFilterMenuItem(string menuHeader)
    {
        ImportFilters importFilters = new();
        Menu filterMenu = importFilters.FilterMenu;
        MenuItem menuItem = new() { Header = menuHeader };
        filterMenu.Items.Add(menuItem);

        importFilters.Show();

        return menuItem;
    }

    private void SeriesButton_OnClick(object sender, RoutedEventArgs e)
    {
        _menuHelper.InitializeSeriesMenu(GetFilterMenuItem("Series"), menuStaysOpen: true);
    }

    private void GenreButton_OnClick(object sender, RoutedEventArgs e)
    {
        _menuHelper.InitializeGenresMenu(GetFilterMenuItem("Genre"), menuStaysOpen: true);
    }

    private void PlatformButton_OnClick(object sender, RoutedEventArgs e)
    {
        _menuHelper.InitializePlatformsMenu(GetFilterMenuItem("Platforms"), menuStaysOpen: true);
    }

    private void DeveloperButton_OnClick(object sender, RoutedEventArgs e)
    {
        _menuHelper.InitializeDevelopersMenu(GetFilterMenuItem("Developers"), menuStaysOpen: true);
    }

    private void PublisherButton_OnClick(object sender, RoutedEventArgs e)
    {
        _menuHelper.InitializePublishersMenu(GetFilterMenuItem("Publishers"), menuStaysOpen: true);
    }
}
