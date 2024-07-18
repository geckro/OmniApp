using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

public interface IDataGridHelper
{
    Task PopulateGameDataGridAsync(DataGrid dataGrid);
    Task RefreshGameDataGridAsync();
}

/// <summary>
///     Helper for the DataGrid control.
/// </summary>
public class DataGridHelper(
    IMetadataAccessor<Game> gameAccessor,
    IMetadataAccessor<Genre> genreAccessor,
    IMetadataAccessor<Developer> developerAccessor,
    IMetadataAccessor<Publisher> publisherAccessor,
    IMetadataAccessor<Platform> platformAccessor,
    IMetadataAccessor<Series> seriesAccessor)
    : IDataGridHelper
{
    private DataGrid _dataGrid = null!;
    private ICollection<Game> _games = [];

    /// <summary>
    ///     Populates the main Game DataGrid on the MainGameWindow.
    /// </summary>
    /// <param name="dataGrid">The Game DataGrid.</param>
    public async Task PopulateGameDataGridAsync(DataGrid dataGrid)
    {
        _dataGrid = dataGrid;
        _games = await Task.Run(gameAccessor.LoadMetadataCollection);

        ConfigureGameDataGrid();
        await RefreshGameDataGridAsync();
    }

    /// <summary>
    ///     Refreshes the main Game DataGrid on the MainGameWindow.
    /// </summary>
    public async Task RefreshGameDataGridAsync()
    {
        _games = await Task.Run(gameAccessor.LoadMetadataCollection);
        _dataGrid.ItemsSource = null;
        _dataGrid.ItemsSource = _games;
    }

    private void ConfigureGameDataGrid()
    {
        _dataGrid.AutoGenerateColumns = false;
        _dataGrid.Columns.Clear();

        DataGridColumn[] columns =
        [
            new DataGridTextColumn { Header = "Title", Binding = new Binding(nameof(Game.Title)) },
            CreateBooleanColumn("Played", nameof(Game.HasPlayed)),
            CreateBooleanColumn("Finished", nameof(Game.HasFinished)),
            CreateBooleanColumn("Complete", nameof(Game.HasCompleted)),
            CreateAsyncColumn("Platforms", nameof(Game.Platforms), new AsyncCollectionToStringConverter<Platform>(platformAccessor)),
            CreateAsyncColumn("Genres", nameof(Game.Genres), new AsyncCollectionToStringConverter<Genre>(genreAccessor)),
            CreateAsyncColumn("Developers", nameof(Game.Developers), new AsyncCollectionToStringConverter<Developer>(developerAccessor)),
            CreateAsyncColumn("Publishers", nameof(Game.Publishers), new AsyncCollectionToStringConverter<Publisher>(publisherAccessor)),
            CreateAsyncColumn("Series", nameof(Game.Series), new AsyncCollectionToStringConverter<Series>(seriesAccessor)),
            new DataGridTextColumn { Header = "Date", Binding = new Binding(nameof(Game.ReleaseDateWw)) { StringFormat = "d" } }
        ];

        foreach (DataGridColumn column in columns)
        {
            _dataGrid.Columns.Add(column);
        }
    }

    private static DataGridTextColumn CreateAsyncColumn(string header, string propertyName, IValueConverter converter)
    {
        return new DataGridTextColumn { Header = header, Binding = new Binding(propertyName) { Converter = converter } };
    }

    private static DataGridTemplateColumn CreateBooleanColumn(string header, string propertyName)
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
}

public class AsyncCollectionToStringConverter<T>(IMetadataAccessor<T> accessor) : IValueConverter
    where T : IMetadata
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not IEnumerable<IMetadata> collection ? string.Empty : GetNamedCollectionAsync(collection).GetAwaiter().GetResult();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private async Task<string> GetNamedCollectionAsync(IEnumerable<IMetadata> collection)
    {
        string[] names = await Task.WhenAll(collection.Select(async item =>
        {
            T? fullItem = await Task.FromResult(accessor.GetItemById(item.Id));
            return GetItemName(fullItem) ?? item.Id.ToString();
        }));
        return string.Join(", ", names);
    }

    private static string? GetItemName(IMetadata? item)
    {
        return item switch
        {
            Game game => game.Title,
            Genre genre => genre.Name,
            Platform platform => platform.Name,
            Developer dev => dev.Name,
            Publisher pub => pub.Name,
            Series series => series.Name,
            _ => null
        };
    }
}

/// <summary>
///     Converter to display True/False as ✅ or ❌.
/// </summary>
public class BooleanYesNoConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool boolValue ? boolValue ? "✅" : "❌" : string.Empty;
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
