using GameManager.Core.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    public AddGame()
    {
        InitializeComponent();

        InitializeListView(GenresListView, new GenreData().Deserialize());
        InitializeListView(PlatformsListView, new PlatformData().Deserialize());
        InitializeListView(DevelopersListView, new DeveloperData().Deserialize());
        InitializeListView(PublishersListView, new PublisherData().Deserialize());
        InitializeListView(SeriesListView, new SeriesData().Deserialize());
    }

    private void InitializeListView<T>(ListView listView, IEnumerable<T> metadataList) where T : IMetadata
    {
        foreach (T metadata in metadataList)
        {
            CheckBox checkBox = new() { Content = metadata.Name };
            listView.Items.Add(checkBox);
        }
    }

    private void AddNewGameFinish_Click(object sender, RoutedEventArgs e)
    {
        // string title = TitleBox.Text;
        //
        // if (string.IsNullOrWhiteSpace(title))
        // {
        //     MessageBox.Show(
        //         "Please enter a valid title.",
        //         "Error",
        //         MessageBoxButton.OK,
        //         MessageBoxImage.Error
        //     );
        //     return;
        // }
        //
        // DateTime? dateWw = Date.SelectedDate;
        //
        // Game newGame = new()
        // {
        //     Title = title,
        //     Genres = genres,
        //     Platforms = platforms,
        //     Developers = developers,
        //     Publishers = publishers,
        //     Series = series,
        //     ReleaseDateWw = dateWw,
        //     CreatedOn = DateTime.Now,
        //     LastUpdated = DateTime.Now
        // };
        //
        // GameData gameData = new();
        // gameData.Add(newGame);
    }
}
