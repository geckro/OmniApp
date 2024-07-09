using GameManager.Core.Data;
using GameManager.UI.Helpers;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        UpdateGameDataGrid();

        Menus menuHelper = new();
        menuHelper.InitializePlatformsMenu(PlatformFilter, FilterListBox);
        menuHelper.InitializeGenresMenu(GenreFilter, FilterListBox);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Add());
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Import());
    }

    private void FilterListBox(object sender, RoutedEventArgs e)
    {
        DataGrid gameDataGrid = GameDataGrid;

        List<Genre> selectedGenres = GenreFilter.Items
            .OfType<MenuItem>()
            .Where(item => item.IsChecked)
            .Select(item => new Genre(item.Header.ToString()!))
            .ToList();

        List<Platform> selectedPlatforms = PlatformFilter.Items
            .OfType<MenuItem>()
            .Where(item => item.IsChecked)
            .Select(item => new Platform(item.Header.ToString()!))
            .ToList();

        foreach (object? row in gameDataGrid.Items)
        {
            Game game = (row as DataRowView)?.Row.ItemArray[0] as Game ?? throw new InvalidOperationException();

            bool shouldHide = false;

            if (selectedPlatforms.Count > 0 && game.Platforms != null)
            {
                shouldHide = !game.Platforms.Any(p => selectedPlatforms.Any(sp => sp.Name == p.Name));
            }

            if (selectedGenres.Count > 0 && game.Genres != null)
            {
                shouldHide = shouldHide || !game.Genres.Any(g => selectedGenres.Any(sg => sg.Name == g.Name));
            }

            if (gameDataGrid.ItemContainerGenerator.ContainerFromItem(row) is DataGridRow rowItem)
            {
                rowItem.Visibility = shouldHide ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }

    public void UpdateGameDataGrid()
    {
        DataGrid gameDataGrid = GameDataGrid;
        gameDataGrid.Items.Clear();
        GameData gameData = new();

        var gameRows = gameData.GetGames().Select(game => new
        {
            game.Title,
            Genres = game.Genres != null ? string.Join(", ", game.Genres.Select(g => g.Name)) : "",
            Platforms =
                game.Platforms != null
                    ? string.Join(", ",
                        game.Platforms.Select(p =>
                            !string.IsNullOrEmpty(p.Company) ? $"{p.Company} - {p.Name}" : p.Name))
                    : "",
            Date = game.ReleaseDateWw.HasValue ? game.ReleaseDateWw.Value.ToString("yyyy-MMMM-dd") : "",
            Developers = game.Developers != null ? string.Join(", ", game.Developers.Select(d => d.Name)) : "",
            Publishers = game.Publishers != null ? string.Join(", ", game.Publishers.Select(p => p.Name)) : "",
            Series = game.Series != null ? string.Join(", ", game.Series.Select(s => s.Name)) : ""
        }).ToList();

        GameDataGrid.ItemsSource = gameRows;
    }
}
