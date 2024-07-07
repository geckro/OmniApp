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
    }

    private void AddNewGenre_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AddNewGameFinish_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void PlatformsMenu_OnClick(object sender, RoutedEventArgs e)
    {
        List<Platform> platforms = _platformData.Deserialize();

        PlatformsMenu.Items.Clear();

        foreach (Platform platform in platforms)
        {
            MenuItem menuItem = new()
            {
                Header = platform.Name
            };
            PlatformsMenu.Items.Add(menuItem);
        }
    }

    private void GenresMenu_OnClick(object sender, RoutedEventArgs e)
    {
        List<Genre> genres = _genreData.Deserialize();

        GenresMenu.Items.Clear();

        foreach (Genre genre in genres)
        {
            MenuItem menuItem = new()
            {
                Header = genre.Name
            };
            GenresMenu.Items.Add(menuItem);
        }
    }
}