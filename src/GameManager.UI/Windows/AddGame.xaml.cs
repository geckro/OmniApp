using GameManager.Core.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class AddGame
{
    private readonly Dictionary<string, bool> _checkedStates = new();
    public AddGame()
    {
        InitializeComponent();

        DataManagerFactory dmf = new();
        MakeMetadataAreas("Genre", GenreStackPanel, dmf.CreateData<Genre>().ReadFromJson());
        MakeMetadataAreas("Platform", PlatformStackPanel, dmf.CreateData<Platform>().ReadFromJson());
        MakeMetadataAreas("Developer", DeveloperStackPanel, dmf.CreateData<Developer>().ReadFromJson());
        MakeMetadataAreas("Publisher", PublisherStackPanel, dmf.CreateData<Publisher>().ReadFromJson());
        MakeMetadataAreas("Series", SeriesStackPanel, dmf.CreateData<Series>().ReadFromJson());

        MakeMetadataAreas("Composer", ComposerStackPanel, dmf.CreateData<Composer>().ReadFromJson());
        MakeMetadataAreas("Director", DirectorStackPanel, dmf.CreateData<Director>().ReadFromJson());
        MakeMetadataAreas("Engine", EngineStackPanel, dmf.CreateData<Engine>().ReadFromJson());
        MakeMetadataAreas("Writer", WriterStackPanel, dmf.CreateData<Writer>().ReadFromJson());
        MakeMetadataAreas("AgeRating", AgeRatingStackPanel, dmf.CreateData<AgeRatings>().ReadFromJson());
        MakeMetadataAreas("Producer", ProducerStackPanel, dmf.CreateData<Producer>().ReadFromJson());
        MakeMetadataAreas("Designer", DesignerStackPanel, dmf.CreateData<Designer>().ReadFromJson());
        MakeMetadataAreas("Programmer", ProgrammerStackPanel, dmf.CreateData<Programmer>().ReadFromJson());
        MakeMetadataAreas("Artist", ArtistStackPanel, dmf.CreateData<Artist>().ReadFromJson());
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
        // string title = TitleBox.Text;
        //
        // if (string.IsNullOrWhiteSpace(title))
        // {
        //     MessageBox.Show(
        //         "Please enter a valid title for the game.",
        //         "Error",
        //         MessageBoxButton.OK,
        //         MessageBoxImage.Error
        //     );
        //     return;
        // }

        // DateTime? dateWw = Date.SelectedDate;


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


        // DataManagerFactory factory = new();
        // JsonData<Game> gameData = factory.CreateData<Game>();
        //
        // ICollection<Game> games = gameData.ReadFromJson();
        // games.Add(newGame);
        // gameData.WriteJson(games);
    }

    private void MakeMetadataAreas<T>(string name, StackPanel stackPanel, ICollection<T> dataSource) where T : IMetadata
    {
        Label label = new() { Content = name };

        TextBox textBox = new() { Name = $"{name}TextBox" };
        RegisterName(textBox.Name, textBox);
        textBox.TextChanged += (sender, e) => TextBox_TextChanged(sender, e, dataSource);

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
}
