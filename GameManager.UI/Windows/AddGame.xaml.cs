using System.Windows;
using System.Windows.Controls;
using GameManager.Core.Data;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    private readonly GenreData _genreData;
    private readonly PlatformData _platformData;

    public AddGame()
    {
        InitializeComponent();
        _genreData = new GenreData();
        _platformData = new PlatformData();

        InitializePlatformsMenu();
        InitializeGenresMenu();
    }

    private void AddNewGameFinish_Click(object sender, RoutedEventArgs e)
    {
        string title = TitleBox.Text;

        if (string.IsNullOrWhiteSpace(title))
        {
            MessageBox.Show(
                "Please enter a valid title.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
            return;
        }

        List<Genre> genres = (from MenuItem item in GenresMenu.Items
            where item.IsChecked
            select new Genre(item.Header.ToString())).ToList();

        List<Platform> platforms = (from MenuItem item in PlatformsMenu.Items
            where item.IsChecked
            select new Platform(item.Header.ToString())).ToList();

        GameData gameData = new(title, genres.ToArray(), platforms.ToArray());

        gameData.Serialize();
    }

    private void InitializePlatformsMenu()
    {
        List<Platform> platforms = _platformData.Deserialize();

        PlatformsMenu.Items.Clear();

        foreach (Platform platform in platforms)
        {
            MenuItem menuItem = new()
            {
                Header = platform.Name,
                IsCheckable = true
            };
            PlatformsMenu.Items.Add(menuItem);
        }
    }

    private void InitializeGenresMenu()
    {
        List<Genre> genres = _genreData.Deserialize();

        GenresMenu.Items.Clear();

        foreach (Genre genre in genres)
        {
            MenuItem menuItem = new()
            {
                Header = genre.Name,
                IsCheckable = true
            };
            GenresMenu.Items.Add(menuItem);
        }
    }
}