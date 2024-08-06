using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class GameManagerPrefsWindow
{
    private readonly MetadataAccessor<Game> _gameAcc;

    public GameManagerPrefsWindow(MetadataAccessor<Game> gameAcc)
    {
        _gameAcc = gameAcc;

        InitializeComponent();
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await PopulateCheckBoxesInStackPanelAsync();
    }

    private async Task PopulateCheckBoxesInStackPanelAsync()
    {
        HashSet<string?> properties = [];

        foreach (Game game in _gameAcc.LoadMetadata())
        {
            ICollection<string?> gameProperties = await Task.Run(() => _gameAcc.GetAllProperties(game));

            foreach (string? property in gameProperties)
            {
                properties.Add(property);
            }
        }

        foreach (CheckBox checkBox in properties.Select(property => new CheckBox { Content = property }))
        {
            CheckBoxTableColumnsStackPanel.Children.Add(checkBox);
        }
    }
}
