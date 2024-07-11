using GameManager.Core.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    public AddGame()
    {
        InitializeComponent();

        InitializeListView(GenresListView, new GenreData().ReadFromJson());
        InitializeListView(PlatformsListView, new PlatformData().ReadFromJson());
        InitializeListView(DevelopersListView, new DeveloperData().ReadFromJson());
        InitializeListView(PublishersListView, new PublisherData().ReadFromJson());
        InitializeListView(SeriesListView, new SeriesData().ReadFromJson());
    }

    private static void InitializeListView<T>(ListView listView, IEnumerable<T> metadataList) where T : IMetadata
    {
        foreach (T metadata in metadataList)
        {
            CheckBox checkBox = new() { Content = metadata.Name };
            listView.Items.Add(checkBox);
        }
    }

    private static List<T> ExtractCheckBoxes<T>(ListView listView, Func<string, T> metadataFactory)
    {
        return listView.Items
            .OfType<CheckBox>()
            .Where(item => item.IsChecked == true)
            .Select(item => metadataFactory(item.Name))
            .ToList();
    }

    private void AddNewGameFinish_Click(object sender, RoutedEventArgs e)
    {
        string title = TitleBox.Text;

        if (string.IsNullOrWhiteSpace(title))
        {
            MessageBox.Show(
                "Please enter a valid title for the game.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
            return;
        }

        DateTime? dateWw = Date.SelectedDate;

        List<Genre> genres = ExtractCheckBoxes(GenresListView, name => new Genre(name));
        List<Developer> developers = ExtractCheckBoxes(DevelopersListView, name => new Developer(name));
        List<Publisher> publishers = ExtractCheckBoxes(PublishersListView, name => new Publisher(name));
        List<Series> series = ExtractCheckBoxes(SeriesListView, name => new Series(name));
        List<Platform> platforms = ExtractCheckBoxes(PlatformsListView, name => new Platform { Name = name });

        Game newGame = new()
        {
            Title = title,
            Genres = genres,
            Platforms = platforms,
            Developers = developers,
            Publishers = publishers,
            Series = series,
            ReleaseDateWw = dateWw,
            CreatedOn = DateTime.Now,
            LastUpdated = DateTime.Now
        };

        GameData gameData = new();

        ICollection<Game> games = gameData.ReadFromJson();
        games.Add(newGame);
        gameData.WriteJson(games);
    }
}
