using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class GameFilterHelper
{
    private readonly Dictionary<string, ListBox> _categoryListBoxes;
    private readonly ICollection<Game> _data;
    private readonly StackPanel _filterStackPanel;
    private readonly MetadataAccessor<Developer> _developerAccessor;
    private readonly MetadataAccessor<Publisher> _publisherAccessor;
    private readonly MetadataAccessor<Genre> _genreAccessor;
    private readonly MetadataAccessor<Platform> _platformAccessor;
    private readonly MetadataAccessor<Series> _seriesAccessor;
    private MainGameWindowViewModel _viewModel = null!;

    public GameFilterHelper(GameManagerWindow mainWindow, MetadataAccessor<Game> data, MetadataAccessor<Developer> developerData, MetadataAccessor<Publisher> publisherData, MetadataAccessor<Genre> genreData,
        MetadataAccessor<Platform> platformData, MetadataAccessor<Series> seriesData)
    {
        mainWindow.SetFilter(this);
        _filterStackPanel = mainWindow.FilterStackPanel;
        _data = data.LoadMetadataCollection();

        _developerAccessor = developerData;
        _publisherAccessor = publisherData;
        _genreAccessor = genreData;
        _platformAccessor = platformData;
        _seriesAccessor = seriesData;

        _categoryListBoxes = new Dictionary<string, ListBox>
        {
            ["ReleaseDateWw"] = CreateCategoryPanel("Release Date"),
            ["Developers"] = CreateCategoryPanel("Developers"),
            ["Publishers"] = CreateCategoryPanel("Publishers"),
            ["Genres"] = CreateCategoryPanel("Genres"),
            ["Platforms"] = CreateCategoryPanel("Platforms"),
            ["Series"] = CreateCategoryPanel("Series")
        };
    }

    public void SetViewModel(MainGameWindowViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    private ListBox CreateCategoryPanel(string headerText)
    {
        StackPanel panel = new();
        Label header = new()
        {
            Content = headerText,
            FontFamily = StyleHelper.Instance.HeaderFontFamily,
            FontWeight = StyleHelper.Instance.HeaderFontWeight,
            FontSize = StyleHelper.Instance.HeaderFontSize
        };
        headerText = headerText.Replace(" ", "");
        ListBox listBox = new()
        {
            Name = $"{headerText}ListBox",
            MaxHeight = 120,
            Padding = new Thickness(0),
            Margin = new Thickness(0),
            Background = StyleHelper.Instance.ListBoxBackgroundColor,
            ItemContainerStyle = CreateListBoxItemStyle()
        };
        panel.Children.Add(header);
        panel.Children.Add(listBox);
        _filterStackPanel.Children.Add(panel);
        return listBox;
    }

    private static Style CreateListBoxItemStyle()
    {
        Style style = new(typeof(ListBoxItem));
        style.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(2)));
        style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(0)));

        return style;
    }

    public void RefreshAllFilterMenus()
    {
        Logger.Debug(LogClass.GameMgrUi, "Running RefreshAllFilterMenus");

        try
        {
            foreach (KeyValuePair<string, ListBox> listBox in _categoryListBoxes)
            {
                RefreshFilterMenu(listBox.Value, () => PopulatePanel(listBox.Key));
            }

            Logger.Info(LogClass.GameMgrUi, "All filter menus successfully refreshed.");
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error refreshing filter menus: {ex.Message}");
        }
    }

    private static void RefreshFilterMenu(ListBox listBox, Action populatePanel)
    {
        listBox.Items.Clear();
        populatePanel();
    }

    public void PopulateFilterMenus()
    {
        Logger.Debug(LogClass.GameMgrUi, "Initializing PopulateFilterMenus");

        try
        {
            foreach (KeyValuePair<string, ListBox> listBox in _categoryListBoxes)
            {
                PopulatePanel(listBox.Key);
            }

            Logger.Info(LogClass.GameMgrUi, "Filter menus successfully populated.");
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error populating filter menus: {ex.Message}");
        }
    }

    private void PopulatePanel(string category)
    {
        IEnumerable<string> items = category switch
        {
            "ReleaseDateWw" => GetYearItems(),
            "Developers" => GetNamedItems(_data.Where(g => g.Developers != null).SelectMany(g => g.Developers!).Select(d => d.Id).Distinct(), _developerAccessor),
            "Publishers" => GetNamedItems(_data.Where(g => g.Publishers != null).SelectMany(g => g.Publishers!).Select(p => p.Id).Distinct(), _publisherAccessor),
            "Genres" => GetNamedItems(_data.Where(g => g.Genres != null).SelectMany(g => g.Genres!).Select(g => g.Id).Distinct(), _genreAccessor),
            "Platforms" => GetNamedItems(_data.Where(g => g.Platforms != null).SelectMany(g => g.Platforms!).Select(g => g.Id).Distinct(), _platformAccessor),
            "Series" => GetNamedItems(_data.Where(g => g.Series != null).SelectMany(g => g.Series!).Select(g => g.Id).Distinct(), _seriesAccessor),
            _ => throw new ArgumentException($"Invalid category: {category}")
        };

        foreach (string item in items)
        {
            _categoryListBoxes[category].Items.Add(new CheckBox { Content = item, Command = _viewModel.FilterGameTableCommand });
        }
    }

    private IEnumerable<string> GetYearItems()
    {
        return _data.Where(g => g.ReleaseDateWw.HasValue)
            .Select(g => g.ReleaseDateWw!.Value.Year.ToString())
            .Distinct()
            .OrderByDescending(y => y);
    }

    private static IEnumerable<string> GetNamedItems<T>(IEnumerable<Guid> ids, MetadataAccessor<T> accessor) where T : IMetadata
    {
        return ids.Select(accessor.GetItemById)
            .Where(i => i != null && !string.IsNullOrWhiteSpace(i.Name))
            .Select(i => i!.Name)
            .OrderBy(n => n);
    }

    public Dictionary<string, List<string>> GetCheckedFilters()
    {
        Dictionary<string, List<string>> checkedFilters = new()
        {
            ["ReleaseDateWw"] = [],
            ["Developers"] = [],
            ["Publishers"] = [],
            ["Genres"] = [],
            ["Platforms"] = [],
            ["Series"] = []
        };

        foreach (KeyValuePair<string, ListBox> categoryListBox in _categoryListBoxes)
        {
            string category = categoryListBox.Key;
            ListBox listBox = categoryListBox.Value;

            foreach (object? child in listBox.Items)
            {
                if (child is CheckBox { IsChecked: true } checkBox)
                {
                    checkedFilters[category].Add(checkBox.Content.ToString()!);
                }
            }
        }

        return checkedFilters.Where(kvp => kvp.Value.Count > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
