using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.Core.Scrapers;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using OmniApp.Common.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    private readonly MetadataAccessorFactory _metadataAccessorFactory;
    private readonly MetadataPersistence _metadataPersistence = new();

    public AddGame()
    {
        InitializeComponent();
        _metadataAccessorFactory = new MetadataAccessorFactory(_metadataPersistence);
        InitializeMetadataAreas();
    }

    public event EventHandler<Game>? GameAdded;

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
            ReleaseDateWw = Date.SelectedDate,
            CreatedOn = currentTime,
            LastUpdated = currentTime
        };

        MetadataAccessor<Game> gameMetadataAccessor = _metadataAccessorFactory.CreateMetadataAccessor<Game>();

        ICollection<Game> games = gameMetadataAccessor.LoadMetadataCollection();
        games.Add(newGame);
        gameMetadataAccessor.SaveMetadataCollection(games);

        GameAdded?.Invoke(this, newGame);
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

        if (sender is not TextBox textBox)
        {
            return;
        }

        string text = textBox.Text;

        Uri? link = UrlHelper.ConvertStringToUri(text);
        if (link != null)
        {
            Wikipedia wikipedia = new();
            WikipediaPage? result = wikipedia.ScrapePageAsync(link).Result;

            if (result != null)
            {
                Console.WriteLine($"Title: {result.Title}");
                Console.WriteLine($"URL: {result.WikipediaLink}");
                if (result.InfoboxData != null)
                {
                    foreach (KeyValuePair<string, string> item in result.InfoboxData)
                    {
                        Console.WriteLine($"{item.Key}: {item.Value}");
                    }
                }
            }
        }

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
