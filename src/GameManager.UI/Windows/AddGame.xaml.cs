using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    private readonly IMetadataAccessorFactory _metadataAccessorFactory;
    private readonly IMetadataPersistence _metadataPersistence = new MetadataPersistence();

    public AddGame()
    {
        InitializeComponent();
        _metadataAccessorFactory = new MetadataAccessorFactory(_metadataPersistence);
        InitializeMetadataAreas();
    }

    private async void InitializeMetadataAreas()
    {
        await new AddGameMetadataManager(this, _metadataAccessorFactory).InitializeMetadataAreasAsync();
    }

    /// <summary>
    ///     Extracts checked boxes from a ListBox
    /// </summary>
    /// <param name="listBox">The ListBox to extract CheckBoxes from</param>
    /// <param name="metadataFactory">Method to create metadata instances</param>
    /// <typeparam name="T">The type of IMetadata</typeparam>
    /// <returns>Collection of an IMetadata instance</returns>
    /// <exception cref="Exception"></exception>
    private static Collection<T> ExtractCheckBoxes<T>(ListBox? listBox, Func<Guid, T> metadataFactory) where T : IMetadata
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

            if (checkBox.Tag is not Guid guid)
            {
                continue;
            }

            T metadata = metadataFactory(guid);
            result.Add(metadata);
        }

        return result;
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

        DateTime currentTime = DateTime.Now;

        Game newGame = new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Genres = ExtractCheckBoxes((ListBox)FindName("GenreListBox"), id => new Genre { Id = id }),
            Platforms = ExtractCheckBoxes((ListBox)FindName("PlatformListBox"), id => new Platform { Id = id }),
            Developers = ExtractCheckBoxes((ListBox)FindName("DeveloperListBox"), id => new Developer { Id = id }),
            Publishers = ExtractCheckBoxes((ListBox)FindName("PublisherListBox"), id => new Publisher { Id = id }),
            Series = ExtractCheckBoxes((ListBox)FindName("SeriesListBox"), id => new Series { Id = id }),
            Writers = ExtractCheckBoxes((ListBox)FindName("WriterListBox"), id => new Writer { Id = id }),
            Directors = ExtractCheckBoxes((ListBox)FindName("DirectorListBox"), id => new Director { Id = id }),
            Artists = ExtractCheckBoxes((ListBox)FindName("ArtistListBox"), id => new Artist { Id = id }),
            Designers = ExtractCheckBoxes((ListBox)FindName("DesignerListBox"), id => new Designer { Id = id }),
            Programmers = ExtractCheckBoxes((ListBox)FindName("ProgrammerListBox"), id => new Programmer { Id = id }),
            Composers = ExtractCheckBoxes((ListBox)FindName("ComposerListBox"), id => new Composer { Id = id }),
            AgeRatings = ExtractCheckBoxes((ListBox)FindName("AgeRatingListBox"), id => new AgeRatings { Id = id }),
            Engine = ExtractCheckBoxes((ListBox)FindName("EngineListBox"), id => new Engine { Id = id }),
            ReleaseDateWw = Date.SelectedDate,
            CreatedOn = currentTime,
            LastUpdated = currentTime
        };

        IMetadataAccessor<Game> gameMetadataAccessor = _metadataAccessorFactory.CreateMetadataAccessor<Game>();

        ICollection<Game> games = gameMetadataAccessor.LoadMetadataCollection();
        games.Add(newGame);
        gameMetadataAccessor.SaveMetadataCollection(games);
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
