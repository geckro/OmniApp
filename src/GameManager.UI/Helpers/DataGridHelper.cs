using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

public interface IDataGridHelper
{
    Task PopulateGameDataGridAsync(DataGrid dataGrid, IMetadataAccessor<Game> gameMetadataAccessor);
    Task RefreshGameDataGridAsync(IMetadataAccessor<Game> gameMetadataAccessor);
}

/// <summary>
///     Helper for the DataGrid control.
/// </summary>
public class DataGridHelper : IDataGridHelper
{
    private DataGrid _dataGrid = null!;
    private ICollection<GameViewModel> _games = [];

    /// <summary>
    ///     Populates the main Game DataGrid on the MainGameWindow.
    /// </summary>
    /// <param name="dataGrid">The Game DataGrid.</param>
    /// <param name="gameMetadataAccessor">Game data from an instance of DataFactoryManager.</param>
    public async Task PopulateGameDataGridAsync(DataGrid dataGrid, IMetadataAccessor<Game> gameMetadataAccessor)
    {
        _dataGrid = dataGrid;
        ICollection<Game> games = await Task.Run(gameMetadataAccessor.LoadMetadataCollection);
        _games = await ConvertToGameViewModels(games, gameMetadataAccessor);

        _dataGrid.AutoGenerateColumns = false;
        _dataGrid.Columns.Clear();

        ConfigureGameDataGridColumns();
        _dataGrid.ItemsSource = _games;
    }

    private async Task<ICollection<GameViewModel>> ConvertToGameViewModels(ICollection<Game> games, IMetadataAccessor<Game> gameMetadataAccessor)
    {
        return await Task.WhenAll(games.Select(async game => new GameViewModel
        {
            Title = game.Title,
            HasPlayed = game.HasPlayed,
            HasFinished = game.HasFinished,
            HasCompleted = game.HasCompleted,
            Genres = await GetNamedCollectionAsync(game.Genres, gameMetadataAccessor),
            Developers = await GetNamedCollectionAsync(game.Developers, gameMetadataAccessor),
            Publishers = await GetNamedCollectionAsync(game.Publishers, gameMetadataAccessor),
            Series = await GetNamedCollectionAsync(game.Series, gameMetadataAccessor),
            ReleaseDateWw = game.ReleaseDateWw?.ToString() ?? string.Empty
        }));
    }

    private async Task<string> GetNamedCollectionAsync<T>(ICollection<T>? collection, IMetadataAccessor<Game> accessor) where T : IMetadata
    {
        if (collection == null || collection.Count == 0)
        {
            return string.Empty;
        }

        string[] names = await Task.WhenAll(collection.Select(async item =>
        {
            Game? fullItem = await Task.Run(() => accessor.GetItemById(item.Id));
            return GetItemName(fullItem) ?? item.Id.ToString();
        }));

        return string.Join(", ", names);
    }

    private string? GetItemName(IMetadata? item)
    {
        return item switch
        {
            Game game => game.Title,
            Genre genre => genre.Name,
            Developer dev => dev.Name,
            Publisher pub => pub.Name,
            Series series => series.Name,
            _ => null
        };
    }


    /// <summary>
    ///     Refreshes the main Game DataGrid on the MainGameWindow.
    /// </summary>
    /// <param name="gameMetadataAccessor">Game data from an instance of DataFactoryManager.</param>
    public async Task RefreshGameDataGridAsync(IMetadataAccessor<Game> gameMetadataAccessor)
    {
        ICollection<Game> games = await Task.Run(gameMetadataAccessor.LoadMetadataCollection);
        _games = await ConvertToGameViewModels(games, gameMetadataAccessor);
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = _games;
    }

    private void AddTextColumn(string header, string bindingPath)
    {
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = header, Binding = new Binding(bindingPath) });
    }

    private void AddTrueFalseColumn(string header, string propertyName)
    {
        _dataGrid.Columns.Add(CreateTrueFalseColumn(header, propertyName));
    }

    private static DataGridTemplateColumn CreateTrueFalseColumn(string header, string propertyName)
    {
        return new DataGridTemplateColumn
        {
            Header = header,
            CellTemplate = new DataTemplate(typeof(TextBlock))
            {
                VisualTree = new FrameworkElementFactory(typeof(TextBlock)).AddBinding(
                    TextBlock.TextProperty,
                    new Binding(propertyName) { Converter = new BooleanYesNoConverter() }
                )
            }
        };
    }

    private void ConfigureGameDataGridColumns()
    {
        AddTextColumn("Title", nameof(GameViewModel.Title));
        AddTrueFalseColumn("Played", nameof(GameViewModel.HasPlayed));
        AddTrueFalseColumn("Finished", nameof(GameViewModel.HasFinished));
        AddTrueFalseColumn("Complete", nameof(GameViewModel.HasCompleted));
        AddTextColumn("Genres", nameof(GameViewModel.Genres));
        AddTextColumn("Developers", nameof(GameViewModel.Developers));
        AddTextColumn("Publishers", nameof(GameViewModel.Publishers));
        AddTextColumn("Series", nameof(GameViewModel.Series));
        AddTextColumn("Date", nameof(GameViewModel.ReleaseDateWw));
    }
}

/// <summary>
///     Converter to display True/False as ✅ or ❌.
/// </summary>
public class BooleanYesNoConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool boolValue ? (boolValue ? "✅" : "❌") : string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public static class FrameworkElementFactoryExtensions
{
    public static FrameworkElementFactory AddBinding(this FrameworkElementFactory factory, DependencyProperty property, BindingBase binding)
    {
        factory.SetBinding(property, binding);
        return factory;
    }
}

public class GameViewModel
{
    public string Title { get; set; } = string.Empty;
    public bool? HasPlayed { get; set; }
    public bool? HasFinished { get; set; }
    public bool? HasCompleted { get; set; }
    public string Genres { get; set; } = string.Empty;
    public string Developers { get; set; } = string.Empty;
    public string Publishers { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string ReleaseDateWw { get; set; } = string.Empty;
}
