using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UI.Common.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class FilterHelper
{
    private readonly Dictionary<string, ListBox> _categoryListBoxes;
    private readonly ICollection<Game> _gameAcc;
    private readonly MetadataAccessor<Developer> _developerAcc;
    private readonly StackPanel _filterStackPanel;
    private readonly MetadataAccessor<Genre> _genreAcc;
    private readonly MetadataAccessor<Platform> _platformAcc;
    private readonly MetadataAccessor<Publisher> _publisherAcc;
    private readonly MetadataAccessor<Series> _seriesAcc;
    private GameMgrWindowViewModel _viewModel = null!;

    public FilterHelper(GameManagerWindow gameMgrWindow,
            MetadataAccessor<Game> gameAcc,
            MetadataAccessor<Developer> developerAcc,
            MetadataAccessor<Publisher> publisherAcc,
            MetadataAccessor<Genre> genreAcc,
            MetadataAccessor<Platform> platformAcc,
            MetadataAccessor<Series> seriesAcc)
    {
        gameMgrWindow.SetFilter(this);
        _filterStackPanel = gameMgrWindow.FilterStackPanel;
        _gameAcc = gameAcc.LoadMetadata();

        _developerAcc = developerAcc;
        _publisherAcc = publisherAcc;
        _genreAcc = genreAcc;
        _platformAcc = platformAcc;
        _seriesAcc = seriesAcc;

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

    public void SetViewModel(GameMgrWindowViewModel viewModel)
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
                Background = StyleHelper.Instance.ListBackgroundColor,
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
        Logger.Debug(LogClass.GameMgrUiHelpers, "Running RefreshAllFilterMenus");

        try
        {
            foreach (KeyValuePair<string, ListBox> listBox in _categoryListBoxes)
            {
                RefreshFilterMenu(listBox.Value, () => PopulatePanel(listBox.Key));
            }

            Logger.Info(LogClass.GameMgrUiHelpers, "All filter menus successfully refreshed.");
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUiHelpers, $"Error refreshing filter menus: {ex.Message}");
        }
    }

    private static void RefreshFilterMenu(ListBox listBox, Action populatePanel)
    {
        listBox.Items.Clear();
        populatePanel();
    }

    public void PopulateFilterMenus()
    {
        Logger.Debug(LogClass.GameMgrUiHelpers, "Populating Filters...");

        try
        {
            foreach (KeyValuePair<string, ListBox> listBox in _categoryListBoxes)
            {
                PopulatePanel(listBox.Key);
            }

            Logger.Info(LogClass.GameMgrUiHelpers, "Filter menus successfully populated.");
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUiHelpers, $"Error populating filter menus: {ex.Message}");
        }
    }

    private void PopulatePanel(string category)
    {
        Logger.Debug(LogClass.GameMgrUiHelpers, "Populating Panels...");

        IEnumerable<string> items = category switch
        {
                "ReleaseDateWw" => GetYearItems(),
                "Developers" => GetNamedItems(
                        _gameAcc.Where(g => g.Developers != null).SelectMany(g => g.Developers!).Select(d => d.Id)
                                .Distinct(), _developerAcc),
                "Publishers" => GetNamedItems(
                        _gameAcc.Where(g => g.Publishers != null).SelectMany(g => g.Publishers!).Select(p => p.Id)
                                .Distinct(), _publisherAcc),
                "Genres" => GetNamedItems(
                        _gameAcc.Where(g => g.Genres != null).SelectMany(g => g.Genres!).Select(g => g.Id).Distinct(),
                        _genreAcc),
                "Platforms" => GetNamedItems(
                        _gameAcc.Where(g => g.Platforms != null).SelectMany(g => g.Platforms!).Select(g => g.Id)
                                .Distinct(), _platformAcc),
                "Series" => GetNamedItems(
                        _gameAcc.Where(g => g.Series != null).SelectMany(g => g.Series!).Select(g => g.Id).Distinct(),
                        _seriesAcc),
                _ => throw new ArgumentException($"Invalid category: {category}")
        };

        foreach (string item in items)
        {
            _categoryListBoxes[category].Items
                    .Add(new CheckBox { Content = item, Command = _viewModel.FilterGameTableCommand });
        }
    }

    private IEnumerable<string> GetYearItems()
    {
        return _gameAcc.Where(g => g.ReleaseDateWw.HasValue).Select(g => g.ReleaseDateWw!.Value.Year.ToString()).Distinct()
                .OrderByDescending(y => y);
    }

    private static IEnumerable<string> GetNamedItems<T>(IEnumerable<Guid> ids, MetadataAccessor<T> accessor)
            where T : IMetadata
    {
        return ids.Select(accessor.GetItemById).Where(i => i != null && !string.IsNullOrWhiteSpace(i.Name))
                .Select(i => i!.Name).OrderBy(n => n);
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

        foreach ((string? category, ListBox? listBox) in _categoryListBoxes)
        {
            foreach (object? child in listBox.Items)
            {
                if (child is CheckBox { IsChecked: true } checkBox)
                {
                    checkedFilters[category].Add(checkBox.Content.ToString()!);
                }
            }
        }

        return checkedFilters.Where(kvp => kvp.Value.Count > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
