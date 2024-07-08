using System.Windows;
using System.Windows.Controls;
using GameManager.Core.Data;
using GameManager.UI.Helpers;

namespace GameManager.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        UpdateGameListBox();

        Menus menuHelper = new();
        menuHelper.InitializePlatformsMenu(PlatformFilter, FilterListBox);
        menuHelper.InitializeGenresMenu(GenreFilter, FilterListBox);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Add());
    }

    private void FilterListBox(object sender, RoutedEventArgs e)
    {
        ListBox gameListBox = GameListBox;

        List<Genre> selectedGenres = GenreFilter.Items
            .OfType<MenuItem>()
            .Where(item => item.IsChecked)
            .Select(item => new Genre(item.Header.ToString()))
            .ToList();

        List<Platform> selectedPlatforms = PlatformFilter.Items
            .OfType<MenuItem>()
            .Where(item => item.IsChecked)
            .Select(item => new Platform(item.Header.ToString()))
            .ToList();

        foreach (ListBoxItem item in gameListBox.Items)
        {
            Game game = item.DataContext as Game;
            if (game == null) continue;
            bool shouldHide = false;

            if (selectedPlatforms.Count > 0 && game.Platforms != null)
            {
                shouldHide = !game.Platforms.Any(p => selectedPlatforms.Any(sp => sp.Name == p.Name));
            }

            if (selectedGenres.Count > 0 && game.Genres != null)
            {
                shouldHide = shouldHide || !game.Genres.Any(g => selectedGenres.Any(sg => sg.Name == g.Name));
            }

            item.Visibility = shouldHide ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public void UpdateGameListBox()
    {
        ListBox gameListBox = GameListBox;

        gameListBox.Items.Clear();

        GameData gameData = new();

        foreach (Game game in gameData.GetGames())
        {
            ListBoxItem listBoxItem = new()
            {
                Content = $"{game.Title} | " +
                          $"{string.Join(", ", (game.Platforms ?? []).Select(p => p.Name))} | " +
                          $"{string.Join(", ", (game.Genres ?? []).Select(g => g.Name))}" +
                          $"{(game.Date.HasValue ? $" | {game.Date.Value.Date:yyyy-MMMM-dd}" : "")}",
                DataContext = game
            };
            gameListBox.Items.Add(listBoxItem);
        }
    }
}