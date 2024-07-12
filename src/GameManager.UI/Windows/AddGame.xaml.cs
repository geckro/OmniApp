using GameManager.Core.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    private readonly Dictionary<string, bool> _checkedStates = new();

    private readonly Dictionary<char, int> _commonRomanNumerals = new() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 } };

    private readonly DataManagerFactory _dataManagerFactory = new();

    private int _totalRomanNumeralValue;

    public AddGame()
    {
        InitializeComponent();

        MakeMetadataAreas("Genre", GenreStackPanel, _dataManagerFactory.CreateData<Genre>().ReadFromJson());
        MakeMetadataAreas("Platform", PlatformStackPanel, _dataManagerFactory.CreateData<Platform>().ReadFromJson());
        MakeMetadataAreas("Developer", DeveloperStackPanel, _dataManagerFactory.CreateData<Developer>().ReadFromJson());
        MakeMetadataAreas("Publisher", PublisherStackPanel, _dataManagerFactory.CreateData<Publisher>().ReadFromJson());
        MakeMetadataAreas("Series", SeriesStackPanel, _dataManagerFactory.CreateData<Series>().ReadFromJson());

        MakeMetadataAreas("Composer", ComposerStackPanel, _dataManagerFactory.CreateData<Composer>().ReadFromJson());
        MakeMetadataAreas("Director", DirectorStackPanel, _dataManagerFactory.CreateData<Director>().ReadFromJson());
        MakeMetadataAreas("Engine", EngineStackPanel, _dataManagerFactory.CreateData<Engine>().ReadFromJson());
        MakeMetadataAreas("Writer", WriterStackPanel, _dataManagerFactory.CreateData<Writer>().ReadFromJson());
        MakeMetadataAreas("AgeRating", AgeRatingStackPanel,
            _dataManagerFactory.CreateData<AgeRatings>().ReadFromJson());
        MakeMetadataAreas("Producer", ProducerStackPanel, _dataManagerFactory.CreateData<Producer>().ReadFromJson());
        MakeMetadataAreas("Designer", DesignerStackPanel, _dataManagerFactory.CreateData<Designer>().ReadFromJson());
        MakeMetadataAreas("Programmer", ProgrammerStackPanel,
            _dataManagerFactory.CreateData<Programmer>().ReadFromJson());
        MakeMetadataAreas("Artist", ArtistStackPanel, _dataManagerFactory.CreateData<Artist>().ReadFromJson());
    }

    private static Collection<T> ExtractCheckBoxes<T>(ListBox? listBox, Func<string, T> metadataFactory)
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

            if (checkBox?.IsChecked == true)
            {
                result.Add(metadataFactory(checkBox.Content.ToString()));
            }
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

        DateTime? dateWw = Date.SelectedDate;

        ICollection<Genre> genres =
            ExtractCheckBoxes((ListBox)FindName("GenreListBox"), name => new Genre { Name = name });
        ICollection<Platform> platforms =
            ExtractCheckBoxes((ListBox)FindName("PlatformListBox"), name => new Platform { Name = name });
        ICollection<Developer> developers =
            ExtractCheckBoxes((ListBox)FindName("DeveloperListBox"), name => new Developer { Name = name });
        ICollection<Publisher> publishers =
            ExtractCheckBoxes((ListBox)FindName("PublisherListBox"), name => new Publisher { Name = name });
        ICollection<Series> series =
            ExtractCheckBoxes((ListBox)FindName("SeriesListBox"), name => new Series { Name = name });
        ICollection<Writer> writers =
            ExtractCheckBoxes((ListBox)FindName("WriterListBox"), name => new Writer { Name = name });
        ICollection<Director> directors =
            ExtractCheckBoxes((ListBox)FindName("DirectorListBox"), name => new Director { Name = name });
        ICollection<Designer> designers =
            ExtractCheckBoxes((ListBox)FindName("DesignerListBox"), name => new Designer { Name = name });
        ICollection<Artist> artists =
            ExtractCheckBoxes((ListBox)FindName("ArtistListBox"), name => new Artist { Name = name });
        ICollection<Programmer> programmers = ExtractCheckBoxes((ListBox)FindName("ProgrammerListBox"),
            name => new Programmer { Name = name });
        ICollection<Composer> composers =
            ExtractCheckBoxes((ListBox)FindName("ComposerListBox"), name => new Composer { Name = name });
        ICollection<AgeRatings> ageRatings = ExtractCheckBoxes((ListBox)FindName("AgeRatingListBox"),
            name => new AgeRatings { Name = name });
        ICollection<Engine> engines =
            ExtractCheckBoxes((ListBox)FindName("EngineListBox"), name => new Engine { Name = name });

        Game newGame = new()
        {
            Title = title,
            Genres = genres,
            Platforms = platforms,
            Developers = developers,
            Publishers = publishers,
            Series = series,
            Writers = writers,
            Directors = directors,
            Artists = artists,
            Designers = designers,
            Programmers = programmers,
            Composers = composers,
            AgeRatings = ageRatings,
            Engine = engines,
            ReleaseDateWw = dateWw,
            CreatedOn = DateTime.Now,
            LastUpdated = DateTime.Now
        };

        JsonData<Game> gameData = _dataManagerFactory.CreateData<Game>();

        ICollection<Game> games = gameData.ReadFromJson();
        games.Add(newGame);
        gameData.WriteJson(games);
    }

    private void MakeMetadataAreas<T>(string name, StackPanel stackPanel, ICollection<T> dataSource) where T : IMetadata
    {
        Label label = new() { Content = name };

        TextBox textBox = new() { Name = $"{name}TextBox" };
        RegisterName(textBox.Name, textBox);
        textBox.TextChanged += (sender, e) => TextBox_TextChanged(sender, e, dataSource);
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

    private static Style CreateListBoxItemStyle()
    {
        Style style = new(typeof(ListBoxItem));
        style.Setters.Add(new Setter(PaddingProperty, new Thickness(2)));
        style.Setters.Add(new Setter(MarginProperty, new Thickness(0)));

        return style;
    }

    private void TextBox_TextChanged<T>(object sender, TextChangedEventArgs e, ICollection<T> suggestions)
        where T : IMetadata
    {
        TextBox? textBox = sender as TextBox;
        if (textBox == null)
        {
            return;
        }

        string listBoxName = textBox.Name.Replace("TextBox", "ListBox");
        ListBox? listBox = FindName(listBoxName) as ListBox;
        if (listBox == null)
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
            .Select(item => new CheckBox
            {
                Content = item.Name,
                IsChecked = _checkedStates.TryGetValue(item.Name, out bool isChecked) ? isChecked : false
            })
            .ToList();

        listBox.ItemsSource = checkBoxList;
        listBox.Visibility = checkBoxList.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

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

    private void CreateMetadata(Type dataType, string text)
    {
        Dictionary<Type, Func<object>> typeMap = new()
        {
            { typeof(Genre), () => new Genre { Name = text } },
            { typeof(Platform), () => new Platform { Name = text } },
            { typeof(Developer), () => new Developer { Name = text } },
            { typeof(Publisher), () => new Publisher { Name = text } },
            { typeof(Series), () => new Series { Name = text } },
            { typeof(Composer), () => new Composer { Name = text } },
            { typeof(Director), () => new Director { Name = text } },
            { typeof(Writer), () => new Writer { Name = text } },
            { typeof(Producer), () => new Producer { Name = text } },
            { typeof(Programmer), () => new Programmer { Name = text } },
            { typeof(Engine), () => new Engine { Name = text } },
            { typeof(AgeRatings), () => new AgeRatings { Name = text } },
            { typeof(Designer), () => new Designer { Name = text } },
            { typeof(Artist), () => new Artist { Name = text } }
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
