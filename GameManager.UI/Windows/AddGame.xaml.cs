using System.Windows;
using System.Windows.Controls;
using GameManager.Core.Data;
using GameManager.UI.Helpers;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    public AddGame()
    {
        InitializeComponent();

        Menus menuHelper = new();
        menuHelper.InitializePlatformsMenu(PlatformsMenu);
        menuHelper.InitializeGenresMenu(GenresMenu);
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

        Game newGame = new(title, genres.ToArray(), platforms.ToArray());

        GameData gameData = new();
        gameData.Add(newGame);

        MainWindow mainWindow = new();
        mainWindow.UpdateGameListBox();
    }
}