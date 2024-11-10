using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.Core.Scrapers;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class AddGameWindow
{
    private readonly MetadataAccessorFactory _mtdAccessorFactory;
    private readonly MetadataPersistence _mtdPersistence = new();
    private readonly AddGameViewModel _viewModel;

    public AddGameWindow(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<AddGameViewModel>();
        DataContext = _viewModel;
        _mtdAccessorFactory = new MetadataAccessorFactory(_mtdPersistence);
        InitializeMetadataAreas();
    }

    public event EventHandler<Game>? GameAdded;

    private void InitializeMetadataAreas()
    {
        AddGameMetadataManager metadataManager = new(_mtdAccessorFactory);
        _viewModel.Initialize(metadataManager);
    }

    private Collection<T> ExtractCheckBoxes<T>(ListBox? listBox, Func<Guid, T> metadataFactory)
            where T : IMetadata
    {
        Collection<T> result = [];

        // Get all selected items of type T from SelectedMetadata
        List<string> selectedItems = _viewModel.SelectedMetadata
                .Where(s => s.StartsWith(typeof(T).Name + ":"))
                .Select(s => s.Split(':')[1].Trim())
                .ToList();

        // Find matching items in CurrentMetadata
        IEnumerable<IMetadata> matchingItems = _viewModel.CurrentMetadata
                .Where(m => m is T && selectedItems.Contains(m.Name));

        foreach (IMetadata item in matchingItems)
        {
            result.Add(metadataFactory(item.Id));
        }

        return result;
    }

    private void AddNewGameFinish_Click(object sender, RoutedEventArgs e)
    {
        string title = TitleBox.Text;

        if (string.IsNullOrWhiteSpace(title))
        {
            MessageBox.Show("Please enter a valid title for the game.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            return;
        }

        DateTime currentTime = DateTime.Now;

        Game gameToAdd = new()
        {
                Id = Guid.NewGuid(),
                Title = title,
                Genres = ExtractCheckBoxes(MetadataListBox, id => new Genre { Id = id }),
                Platforms = ExtractCheckBoxes(MetadataListBox, id => new Platform { Id = id }),
                Developers = ExtractCheckBoxes(MetadataListBox, id => new Developer { Id = id }),
                Publishers = ExtractCheckBoxes(MetadataListBox, id => new Publisher { Id = id }),
                Series = ExtractCheckBoxes(MetadataListBox, id => new Series { Id = id }),
                CreatedOn = currentTime,
                LastUpdated = currentTime,
                Tags = new Dictionary<string, ICollection<string>>() // not actually used
        };

        MetadataAccessor<Game> gameAcc = _mtdAccessorFactory.CreateMetadataAccessor<Game>();

        ICollection<Game> gameCollection = gameAcc.LoadMetadata();
        gameCollection.Add(gameToAdd);
        gameAcc.SaveMetadata(gameCollection);

        GameAdded?.Invoke(this, gameToAdd);
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
            Wikimedia wikimedia = new();
            WikipediaPage? result = wikimedia.ScrapePageAsync(link).Result;

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

    private void MetadataListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (IMetadata addedItem in e.AddedItems)
        {
            string metadataString = $"{addedItem.GetType().Name}: {addedItem.Name}";
            if (!_viewModel.SelectedMetadata.Contains(metadataString))
            {
                _viewModel.SelectedMetadata.Add(metadataString);
            }
        }

        foreach (IMetadata removedItem in e.RemovedItems)
        {
            string metadataString = $"{removedItem.GetType().Name}: {removedItem.Name}";
            _viewModel.SelectedMetadata.Remove(metadataString);
        }
    }
}
