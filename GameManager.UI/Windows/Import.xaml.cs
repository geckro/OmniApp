using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GameManager.Core.Data;
using GameManager.UI.Helpers;

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
        var gameRows = GameImports.GetGames().Select(game => new
        {
            game.Title,
            Genres = game.Genres != null ? string.Join(", ", game.Genres.Select(g => g.Name)) : "",
            Platforms = game.Platforms != null ? string.Join(", ", game.Platforms.Select(p => !string.IsNullOrEmpty(p.Company) ? $"{p.Company} - {p.Name}" : p.Name)) : "",
            Date = game.DateWw.HasValue ? game.DateWw.Value.ToString("yyyy-MMMM-dd") : "",
            Developers = game.Developers != null ? string.Join(", ", game.Developers.Select(d => d.Name)) : "",
            Publishers = game.Publishers != null ? string.Join(", ", game.Publishers.Select(p => p.Name)) : "",
            Series = game.Series != null ? string.Join(", ", game.Series.Select(s => s.Name)) : ""
        }).ToList();

        ImportGameDataGrid.ItemsSource = gameRows;

        ICollectionView collectionView = CollectionViewSource.GetDefaultView(ImportGameDataGrid.ItemsSource);
        if (collectionView is not { CanSort: true }) return;
        collectionView.SortDescriptions.Clear();
        collectionView.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
    }

    private static MenuItem GetFilterMenuItem(string menuHeader)
    {
        ImportFilters importFilters = new();
        Menu filterMenu = importFilters.FilterMenu;
        MenuItem menuItem = new()
        {
            Header = menuHeader
        };
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