using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.Windows;

/// <summary>
///     Logic for AddGame.xaml
/// </summary>
public partial class AddGame
{
    private readonly DataManagerFactory _dataManagerFactory = new();

    /// <summary>
    ///     Initializes a new instance of the AddGame class.
    /// </summary>
    public AddGame()
    {
        InitializeComponent();
        new AddGameMetadataManager(this, _dataManagerFactory).InitializeMetadataAreas();
    }

    /// <summary>
    ///     Extracts checked boxes from a ListBox
    /// </summary>
    /// <param name="listBox">The ListBox to extract CheckBoxes from</param>
    /// <param name="metadataFactory">Method to create metadata instances</param>
    /// <typeparam name="T">The type of IMetadata</typeparam>
    /// <returns>Collection of an IMetadata instance</returns>
    /// <exception cref="Exception"></exception>
    private static Collection<T> ExtractCheckBoxes<T>(ListBox? listBox, Func<Guid, string, T> metadataFactory) where T : IMetadata
    {
        if (listBox == null)
        {
            throw new Exception($"ListBox {listBox} does not exist");
        }

        Collection<T> result = [];

        foreach (object? item in listBox.Items)
        {
            CheckBox? checkBox = item switch
            {
                CheckBox cb => cb,
                ListBoxItem lbi => lbi.Content as CheckBox,
                _ => null
            };

            if (checkBox?.IsChecked != true)
            {
                continue;
            }

            if (checkBox.Tag is not Guid guid || checkBox.Content is not string name)
            {
                continue;
            }

            T metadata = metadataFactory(guid, name);
            result.Add(metadata);
        }

        return result;
    }

    /// <summary>
    ///     Handles the click event of the finish button to add a new Game
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">RoutedEventArgs event data</param>
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

        DateTime currentTime = DateTime.Now;

        Game newGame = new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Genres = ExtractCheckBoxes((ListBox)FindName("GenreListBox"), (id, name) => new Genre { Id = id, Name = name }),
            Platforms = ExtractCheckBoxes((ListBox)FindName("PlatformListBox"), (id, name) => new Platform { Id = id, Name = name }),
            Developers = ExtractCheckBoxes((ListBox)FindName("DeveloperListBox"), (id, name) => new Developer { Id = id, Name = name }),
            Publishers = ExtractCheckBoxes((ListBox)FindName("PublisherListBox"), (id, name) => new Publisher { Id = id, Name = name }),
            Series = ExtractCheckBoxes((ListBox)FindName("SeriesListBox"), (id, name) => new Series { Id = id, Name = name }),
            Writers = ExtractCheckBoxes((ListBox)FindName("WriterListBox"), (id, name) => new Writer { Id = id, Name = name }),
            Directors = ExtractCheckBoxes((ListBox)FindName("DirectorListBox"), (id, name) => new Director { Id = id, Name = name }),
            Artists = ExtractCheckBoxes((ListBox)FindName("ArtistListBox"), (id, name) => new Artist { Id = id, Name = name }),
            Designers = ExtractCheckBoxes((ListBox)FindName("DesignerListBox"), (id, name) => new Designer { Id = id, Name = name }),
            Programmers = ExtractCheckBoxes((ListBox)FindName("ProgrammerListBox"), (id, name) => new Programmer { Id = id, Name = name }),
            Composers = ExtractCheckBoxes((ListBox)FindName("ComposerListBox"), (id, name) => new Composer { Id = id, Name = name }),
            AgeRatings = ExtractCheckBoxes((ListBox)FindName("AgeRatingListBox"), (id, name) => new AgeRatings { Id = id, Name = name }),
            Engine = ExtractCheckBoxes((ListBox)FindName("EngineListBox"), (id, name) => new Engine { Id = id, Name = name }),
            ReleaseDateWw = Date.SelectedDate,
            CreatedOn = currentTime,
            LastUpdated = currentTime
        };

        JsonData<Game> gameData = _dataManagerFactory.CreateData<Game>();

        ICollection<Game> games = gameData.ReadFromJson();
        games.Add(newGame);
        gameData.WriteJson(games);
    }

    /// <summary>
    ///     Automatically renames the Title when you press enter.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">KeyEventArgs event data</param>
    private void TitleBox_Rename(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        TextBox textBox = sender as TextBox;
        if (textBox == null)
        {
            return;
        }

        string text = textBox.Text;

        IEnumerable<string> words = text.Split(' ');
        ICollection<string> capitalizedWords = [];

        ICollection<string> wordsToNotCapitalize =
        [
            "and",
            "of",
            "the",
            "vs",
            "vs."
        ];

        foreach (string word in words)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                continue;
            }

            if (wordsToNotCapitalize.Contains(word.ToLower()))
            {
                capitalizedWords.Add(word);
                continue;
            }

            NumberHelper numberHelper = new();
            if (numberHelper.IsRomanNumeral(word))
            {
                capitalizedWords.Add(numberHelper.TotalRomanNumeralValue.ToString());
                continue;
            }

            capitalizedWords.Add(word[0].ToString().ToUpper() + word[1..]);
        }

        textBox.Text = string.Join(' ', capitalizedWords);
    }
}
