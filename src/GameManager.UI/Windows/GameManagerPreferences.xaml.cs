using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class GameManagerPreferences
{
    private readonly MetadataAccessor<Game> _gameMetadataAccessor;

    public GameManagerPreferences(MetadataAccessor<Game> gameMetadataAccessor)
    {
        _gameMetadataAccessor = gameMetadataAccessor;

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

        foreach (Game game in _gameMetadataAccessor.LoadMetadataCollection())
        {
            ICollection<string?> gameProperties = await Task.Run(() => _gameMetadataAccessor.GetAllProperties(game));

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
