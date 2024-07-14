using GameManager.Core.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.Windows;

/// <summary>
///     Logic for AddGame.xaml
/// </summary>
public partial class AddGame
{
    private readonly Dictionary<string, bool> _checkedStates = new();
    private readonly Dictionary<char, int> _commonRomanNumerals = new() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 } };
    private readonly DataManagerFactory _dataManagerFactory = new();
    private int _totalRomanNumeralValue;

    /// <summary>
    ///     Initializes a new instance of the AddGame class.
    /// </summary>
    public AddGame()
    {
        InitializeComponent();
        InitializeMetadataAreas();
    }

    /// <summary>
    ///     Initializes the metadata areas for various Game categories.
    /// </summary>
    private void InitializeMetadataAreas()
    {
        MakeMetadataAreas("Genre", GenreStackPanel, _dataManagerFactory.CreateData<Genre>().ReadFromJson());
        MakeMetadataAreas("Platform", PlatformStackPanel, _dataManagerFactory.CreateData<Platform>().ReadFromJson());
        MakeMetadataAreas("Developer", DeveloperStackPanel, _dataManagerFactory.CreateData<Developer>().ReadFromJson());
        MakeMetadataAreas("Publisher", PublisherStackPanel, _dataManagerFactory.CreateData<Publisher>().ReadFromJson());
        MakeMetadataAreas("Series", SeriesStackPanel, _dataManagerFactory.CreateData<Series>().ReadFromJson());
        MakeMetadataAreas("Composer", ComposerStackPanel, _dataManagerFactory.CreateData<Composer>().ReadFromJson());
        MakeMetadataAreas("Director", DirectorStackPanel, _dataManagerFactory.CreateData<Director>().ReadFromJson());
        MakeMetadataAreas("Engine", EngineStackPanel, _dataManagerFactory.CreateData<Engine>().ReadFromJson());
        MakeMetadataAreas("Writer", WriterStackPanel, _dataManagerFactory.CreateData<Writer>().ReadFromJson());
        MakeMetadataAreas("AgeRating", AgeRatingStackPanel, _dataManagerFactory.CreateData<AgeRatings>().ReadFromJson());
        MakeMetadataAreas("Producer", ProducerStackPanel, _dataManagerFactory.CreateData<Producer>().ReadFromJson());
        MakeMetadataAreas("Designer", DesignerStackPanel, _dataManagerFactory.CreateData<Designer>().ReadFromJson());
        MakeMetadataAreas("Programmer", ProgrammerStackPanel, _dataManagerFactory.CreateData<Programmer>().ReadFromJson());
        MakeMetadataAreas("Artist", ArtistStackPanel, _dataManagerFactory.CreateData<Artist>().ReadFromJson());
    }

    /// <summary>
    ///     Creates metadata areas for a specific category.
    /// </summary>
    /// <param name="name">The name of the metadata category.</param>
    /// <param name="stackPanel">The stack panel to add the generated controls to.</param>
    /// <param name="dataSource">The actual data source of the metadata.</param>
    /// <typeparam name="T">The type of IMetadata.</typeparam>
    private void MakeMetadataAreas<T>(string name, StackPanel stackPanel, ICollection<T> dataSource) where T : IMetadata
    {
        Label label = new() { Content = name };

        TextBox textBox = new() { Name = $"{name}TextBox" };
        RegisterName(textBox.Name, textBox);
        textBox.TextChanged += (sender, _) => TextBox_TextChanged(sender, dataSource);
        textBox.KeyDown += (sender, e) => TextBox_KeyDown(sender, e, typeof(T));

        ListBox listBox = new()
        {
            Name = $"{name}ListBox",
            MaxHeight = 250,
            Visibility = Visibility.Collapsed,
            Padding = new Thickness(0),
            Margin = new Thickness(0),
            FontSize = 14,
            ItemContainerStyle = CreateListBoxItemStyle()
        };
        RegisterName(listBox.Name, listBox);

        stackPanel.Children.Add(label);
        stackPanel.Children.Add(textBox);
        stackPanel.Children.Add(listBox);
    }

    /// <summary>
    ///     Creates a style for the list box item for the metadata areas.
    /// </summary>
    /// <returns>The created list box item style.</returns>
    private static Style CreateListBoxItemStyle()
    {
        Style style = new(typeof(ListBoxItem));
        style.Setters.Add(new Setter(PaddingProperty, new Thickness(2)));
        style.Setters.Add(new Setter(MarginProperty, new Thickness(0)));

        return style;
    }

    /// <summary>
    ///     Handles the TextChanged event of the current text box.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="suggestions">The collection of suggestions to show.</param>
    /// <typeparam name="T">The type of IMetadata.</typeparam>
    private void TextBox_TextChanged<T>(object sender, ICollection<T> suggestions) where T : IMetadata
    {
        if (sender is not TextBox textBox)
        {
            return;
        }

        string listBoxName = textBox.Name.Replace("TextBox", "ListBox");
        if (FindName(listBoxName) is not ListBox listBox)
        {
            return;
        }

        string text = textBox.Text.Trim();

        if (string.IsNullOrEmpty(text))
        {
            listBox.Visibility = Visibility.Collapsed;
            return;
        }

        foreach (CheckBox cb in listBox.Items.OfType<CheckBox>())
        {
            _checkedStates[cb.Content.ToString()] = cb.IsChecked ?? false;
        }

        List<T> filteredSuggestionList = suggestions
            .Where(item => item.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        List<CheckBox> checkBoxList = filteredSuggestionList
            .Select(item => new CheckBox { Content = item.Name, Tag = item.Id, IsChecked = _checkedStates.GetValueOrDefault(item.Name, false) })
            .ToList();

        listBox.ItemsSource = checkBoxList;
        listBox.Visibility = checkBoxList.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>
    ///     Handles the KeyDown event of the TextBox.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="keyEventArgs">KeyEventArgs event data</param>
    /// <param name="dataType">The type of data</param>
    private void TextBox_KeyDown(object sender, KeyEventArgs keyEventArgs, Type dataType)
    {
        if (keyEventArgs.Key != Key.Enter)
        {
            return;
        }

        TextBox textBox = sender as TextBox;
        if (textBox == null)
        {
            return;
        }

        string text = textBox.Text.Trim();

        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        CreateMetadata(dataType, text);

        textBox.Clear();
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
    ///     Create Name metadata for the specified type
    /// </summary>
    /// <param name="dataType">The type to add</param>
    /// <param name="text">The text to add to Name for IMetadata</param>
    private void CreateMetadata(Type dataType, string text)
    {
        Dictionary<Type, Func<object>> typeMap = new()
        {
            { typeof(Genre), () => new Genre { Id = Guid.NewGuid(), Name = text } },
            { typeof(Platform), () => new Platform { Id = Guid.NewGuid(), Name = text } },
            { typeof(Developer), () => new Developer { Id = Guid.NewGuid(), Name = text } },
            { typeof(Publisher), () => new Publisher { Id = Guid.NewGuid(), Name = text } },
            { typeof(Series), () => new Series { Id = Guid.NewGuid(), Name = text } },
            { typeof(Composer), () => new Composer { Id = Guid.NewGuid(), Name = text } },
            { typeof(Director), () => new Director { Id = Guid.NewGuid(), Name = text } },
            { typeof(Writer), () => new Writer { Id = Guid.NewGuid(), Name = text } },
            { typeof(Producer), () => new Producer { Id = Guid.NewGuid(), Name = text } },
            { typeof(Programmer), () => new Programmer { Id = Guid.NewGuid(), Name = text } },
            { typeof(Engine), () => new Engine { Id = Guid.NewGuid(), Name = text } },
            { typeof(AgeRatings), () => new AgeRatings { Id = Guid.NewGuid(), Name = text } },
            { typeof(Designer), () => new Designer { Id = Guid.NewGuid(), Name = text } },
            { typeof(Artist), () => new Artist { Id = Guid.NewGuid(), Name = text } }
        };

        if (!typeMap.TryGetValue(dataType, out Func<object>? createInstance))
        {
            return;
        }

        object instance = createInstance();
        MethodInfo? method = typeof(DataManagerFactory)
            .GetMethod("CreateData")
            ?.MakeGenericMethod(dataType);

        object? data = method?.Invoke(_dataManagerFactory, null);
        data?.GetType().GetMethod("AppendAndWriteJson")?.Invoke(data, [instance]);
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

            if (IsRomanNumeral(word))
            {
                capitalizedWords.Add(_totalRomanNumeralValue.ToString());
                continue;
            }

            capitalizedWords.Add(word[0].ToString().ToUpper() + word[1..]);
        }

        textBox.Text = string.Join(' ', capitalizedWords);
    }

    /// <summary>
    ///     Checks to see if a word is a roman numeral or not
    /// </summary>
    /// <param name="word">The string to check.</param>
    /// <returns>True if it is a roman numeral, False otherwise.</returns>
    private bool IsRomanNumeral(string word)
    {
        // The maximum length roman numeral is XVIII, also add another letter for edge cases
        if (word.Length > 6)
        {
            return false;
        }

        if (word.Any(character => !_commonRomanNumerals.ContainsKey(character)))
        {
            return false;
        }

        int totalValue = 0;
        int previousValue = 0;

        foreach (int currentValue in word.Select(t => _commonRomanNumerals[char.ToUpper(t)]))
        {
            if (currentValue > previousValue)
            {
                totalValue += currentValue - (2 * previousValue);
            }
            else
            {
                totalValue += currentValue;
            }

            previousValue = currentValue;
        }

        _totalRomanNumeralValue = totalValue;
        return totalValue > 0;
    }
}
