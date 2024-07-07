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
        throw new NotImplementedException();
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