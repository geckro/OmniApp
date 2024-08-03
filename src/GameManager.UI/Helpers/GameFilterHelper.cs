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
    private readonly Dictionary<string, StackPanel> _categoryPanels;
    private readonly ICollection<Game> _data;
    private readonly StackPanel _filterStackPanel;
    private readonly MetadataAccessor<Developer> _developerAccessor;
    private readonly MetadataAccessor<Publisher> _publisherAccessor;
    private MainGameWindowViewModel _viewModel;

    public GameFilterHelper(GameManagerWindow mainWindow, MetadataAccessor<Game> data, MetadataAccessor<Developer> developerData, MetadataAccessor<Publisher> publisherData)
    {
        mainWindow.SetFilter(this);
        _filterStackPanel = mainWindow.FilterStackPanel;
        _data = data.LoadMetadataCollection();

        _developerAccessor = developerData;
        _publisherAccessor = publisherData;

        _categoryPanels = new Dictionary<string, StackPanel>
        {
            ["Date"] = CreateCategoryPanel("Release Date"),
            ["Developer"] = CreateCategoryPanel("Developers"),
            ["Publisher"] = CreateCategoryPanel("Publishers")
        };
    }

    public void SetViewModel(MainGameWindowViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    private static StackPanel CreateCategoryPanel(string headerText)
    {
        StackPanel panel = new();
        Label header = new()
        {
            Content = headerText,
            FontWeight = FontWeights.DemiBold
        };
        panel.Children.Add(header);
        return panel;
    }

    public void RefreshAllFilterMenus()
    {
        Logger.Debug(LogClass.GameMgrUi, "Running RefreshAllFilterMenus");

        try
        {
            foreach (KeyValuePair<string, StackPanel> panel in _categoryPanels)
            {
                RefreshFilterMenu(panel.Value, () => PopulatePanel(panel.Key));
            }

            Logger.Info(LogClass.GameMgrUi, "All filter menus successfully refreshed.");
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error refreshing filter menus: {ex.Message}");
        }
    }

    private static void RefreshFilterMenu(StackPanel stackPanel, Action populatePanel)
    {
        stackPanel.Children.Clear();
        populatePanel();
    }

    public void PopulateFilterMenus()
    {
        Logger.Debug(LogClass.GameMgrUi, "Initializing PopulateFilterMenus");

        try
        {
            foreach (KeyValuePair<string, StackPanel> panel in _categoryPanels)
            {
                PopulatePanel(panel.Key);
                _filterStackPanel.Children.Add(panel.Value);
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
            "Date" => GetYearItems(),
            "Developer" => GetNamedItems(_data.Where(g => g.Developers != null).SelectMany(g => g.Developers!).Select(d => d.Id).Distinct(), _developerAccessor),
            "Publisher" => GetNamedItems(_data.Where(g => g.Publishers != null).SelectMany(g => g.Publishers!).Select(p => p.Id).Distinct(), _publisherAccessor),
            _ => throw new ArgumentException($"Invalid category: {category}")
        };

        foreach (string item in items)
        {
            _categoryPanels[category].Children.Add(new CheckBox { Content = item, Command = _viewModel.FilterGameTableCommand });
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
            ["Date"] = [],
            ["Developer"] = [],
            ["Publisher"] = []
        };

        foreach (KeyValuePair<string, StackPanel> categoryPanel in _categoryPanels)
        {
            string category = categoryPanel.Key;
            StackPanel panel = categoryPanel.Value;

            foreach (object? child in panel.Children)
            {
                if (child is CheckBox { IsChecked: true } checkBox)
                {
                    checkedFilters[category].Add(checkBox.Content.ToString());
                }
            }
        }

        return checkedFilters.Where(kvp => kvp.Value.Count > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
