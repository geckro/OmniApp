using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.UI.Common.Helpers;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace GameManager.UI.ViewModels;

public class PreferencesViewModel : ViewModelBase
{
    private readonly MetadataAccessor<Game> _gameAcc;
    public ObservableCollection<string?> TableColumns { get; } = [];
    public ObservableCollection<string?> StyleNames { get; } = [];
    public ObservableCollection<string?> StyleValues { get; } = [];

    public PreferencesViewModel(MetadataAccessor<Game> gameAcc)
    {
        _gameAcc = gameAcc;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await PopulateTableCheckBoxesAsync();
        await PopulateStylesAsync();
    }

    private async Task PopulateTableCheckBoxesAsync()
    {
        TableColumns.Clear();
        foreach (Game game in _gameAcc.LoadMetadata())
        {
            ICollection<string?> gameProperties = await Task.Run(() => _gameAcc.GetAllProperties(game));
            foreach (string? property in gameProperties)
            {
                TableColumns.Add(property);
            }
        }
    }

    private async Task PopulateStylesAsync()
    {
        StyleNames.Clear();
        StyleValues.Clear();

        StyleHelper styleHelperInstance = StyleHelper.Instance;

        foreach (PropertyInfo property in styleHelperInstance.GetType().GetProperties())
        {
            StyleNames.Add(property.Name);

            object? value = property.GetValue(styleHelperInstance);

            string? stringValue = value switch
            {
                    FontFamily fontFamily => fontFamily.ToString(),
                    FontWeight fontWeight => fontWeight.ToString(),
                    SolidColorBrush solidColorBrush => solidColorBrush.Color.ToString(),
                    _ => value?.ToString()
            };

            StyleValues.Add(stringValue);
        }

        if (StyleNames.Count > 0)
        {
            StyleNames.RemoveAt(0);
            StyleValues.RemoveAt(0);
        }

        await Task.CompletedTask;
    }
}
