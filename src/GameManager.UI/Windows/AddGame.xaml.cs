using GameManager.Core.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    public AddGame()
    {
        InitializeComponent();

        DataManagerFactory factory = new();

        InitializeListView(GenresListView, factory.CreateData<Game>().ReadFromJson());
        InitializeListView(PlatformsListView, factory.CreateData<Platform>().ReadFromJson());
        InitializeListView(DevelopersListView, factory.CreateData<Developer>().ReadFromJson());
        InitializeListView(PublishersListView, factory.CreateData<Publisher>().ReadFromJson());
        InitializeListView(SeriesListView, factory.CreateData<Series>().ReadFromJson());
        InitializeListView(DirectorsListView, factory.CreateData<Director>().ReadFromJson());
        InitializeListView(ProducersListView, factory.CreateData<Producer>().ReadFromJson());
        InitializeListView(DesignersListView, factory.CreateData<Designer>().ReadFromJson());
        InitializeListView(ComposersListView, factory.CreateData<Composer>().ReadFromJson());
        InitializeListView(ArtistsListView, factory.CreateData<Artist>().ReadFromJson());
        InitializeListView(ProgrammersListView, factory.CreateData<Programmer>().ReadFromJson());
        InitializeListView(WritersListView, factory.CreateData<Writer>().ReadFromJson());
        InitializeListView(EnginesListView, factory.CreateData<Engine>().ReadFromJson());
        InitializeListView(AgeRatingsListView, factory.CreateData<AgeRatings>().ReadFromJson());
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

        List<Genre> genres = ExtractCheckBoxes(GenresListView, name => new Genre {Name = name });
        List<Developer> developers = ExtractCheckBoxes(DevelopersListView, name => new Developer {Name = name });
        List<Publisher> publishers = ExtractCheckBoxes(PublishersListView, name => new Publisher {Name = name });
        List<Series> series = ExtractCheckBoxes(SeriesListView, name => new Series {Name = name });
        List<Platform> platforms = ExtractCheckBoxes(PlatformsListView, name => new Platform { Name = name});

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


        DataManagerFactory factory = new();
        JsonData<Game> gameData = factory.CreateData<Game>();

        ICollection<Game> games = gameData.ReadFromJson();
        games.Add(newGame);
        gameData.WriteJson(games);
    }
}
