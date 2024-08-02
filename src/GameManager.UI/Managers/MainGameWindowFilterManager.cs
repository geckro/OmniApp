using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Managers;

public class MainGameWindowFilterManager
{
    private readonly ICollection<Game> _data;

    private readonly GameManagerWindow _mainWindow;

    public MainGameWindowFilterManager(GameManagerWindow mainWindow, MetadataAccessor<Game> data)
    {
        _mainWindow = mainWindow;
        _data = data.LoadMetadataCollection();
    }

    public async void PopulateFilterMenu()
    {
        await PopulateDatePanel();
    }

    private async Task PopulateDatePanel()
    {
        StackPanel datePanel = new();

        foreach (Game game in _data)
        {
            if (game.ReleaseDateWw != null)
            {
                int year = game.ReleaseDateWw.Value.Year;
                CheckBox yearCheckBox = new() { Content = year };
                datePanel.Children.Add(yearCheckBox);
            }
        }
        _mainWindow.FilterStackPanel.Children.Add(datePanel);
    }
}
