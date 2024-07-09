using GameManager.Core.Data;
using GameManager.UI.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    public AddGame()
    {
        InitializeComponent();

        Menus menuHelper = new();
        menuHelper.InitializePlatformsMenu(PlatformsMenu);
        menuHelper.InitializeGenresMenu(GenresMenu);
        menuHelper.InitializeDevelopersMenu(DevelopersMenu);
        menuHelper.InitializePublishersMenu(PublishersMenu);
        menuHelper.InitializeSeriesMenu(SeriesMenu);
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

        List<Developer> developers = (from MenuItem item in DevelopersMenu.Items
            where item.IsChecked
            select new Developer(item.Header.ToString())).ToList();

        List<Publisher> publishers = (from MenuItem item in PublishersMenu.Items
            where item.IsChecked
            select new Publisher(item.Header.ToString())).ToList();

        List<Series> series = (from MenuItem item in SeriesMenu.Items
            where item.IsChecked
            select new Series(item.Header.ToString())).ToList();

        DateTime? dateWw = Date.SelectedDate;

        Game newGame = new(
            title,
            genres.Count != 0 ? genres.ToArray() : null,
            platforms.Count != 0 ? platforms.ToArray() : null,
            developers.Count != 0 ? developers.ToArray() : null,
            publishers.Count != 0 ? publishers.ToArray() : null,
            series.Count != 0 ? series.ToArray() : null,
            dateWw
        );

        GameData gameData = new();
        gameData.Add(newGame);
    }
}
