using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.Common.Logging;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

/// <summary>
///     Helper for the DataGrid control.
/// </summary>
public class DataGridHelper(
    MetadataAccessor<Game> gameAccessor,
    MetadataAccessor<Genre> genreAccessor,
    MetadataAccessor<Developer> developerAccessor,
    MetadataAccessor<Publisher> publisherAccessor,
    MetadataAccessor<Platform> platformAccessor,
    MetadataAccessor<Series> seriesAccessor)
{
    private DataGrid _dataGrid = null!;
    private ICollection<Game> _games = [];

    /// <summary>
    ///     Populates the main Game DataGrid on the GameManagerWindow.
    /// </summary>
    /// <param name="dataGrid">The Game DataGrid.</param>
    public async Task PopulateGameDataGridAsync(DataGrid dataGrid)
    {
        Logger.Debug(LogClass.GameMgrUi, "Initializing PopulateGameDataGridAsync");

        try
        {
            _dataGrid = dataGrid;
            _games = await Task.Run(gameAccessor.LoadMetadataCollection);
            ConfigureGameDataGrid();
            await RefreshGameDataGridAsync();

            Logger.Info(LogClass.GameMgrUi, "Game DataGrid successfully populated.");
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error populating Game DataGrid: {ex.Message}");
        }
    }

    /// <summary>
    ///     Refreshes the main Game DataGrid on the GameManagerWindow.
    /// </summary>
    public async Task RefreshGameDataGridAsync()
    {
        Logger.Debug(LogClass.GameMgrUi, "Running RefreshGameDataGridAsync");

        try
        {
            _games = await Task.Run(gameAccessor.LoadMetadataCollection);
            _dataGrid.ItemsSource = null;
            _dataGrid.ItemsSource = _games;

            if (_dataGrid.ItemsSource != null)
            {
                Logger.Info(LogClass.GameMgrUi, "Game DataGrid successfully refreshed.");
            }
            else
            {
                Logger.Warning(LogClass.GameMgrUi, "Game DataGrid did not refresh properly as ItemsSource is null.");
            }
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error refreshing Game DataGrid: {ex.Message}");
        }
    }

    private void ConfigureGameDataGrid()
    {
        Logger.Debug(LogClass.GameMgrUi, "Running ConfigureGameDataGrid.");

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

    public ICollection<string> GetAllVisibleDataGridRowTitle()
    {
        List<string> visibleRowTitles = [];

        foreach (object? item in _dataGrid.Items)
        {
            if (_dataGrid.ItemContainerGenerator.ContainerFromItem(item) is not DataGridRow { Visibility: Visibility.Visible } row)
            {
                continue;
            }

            string? title = (row.DataContext as Game)?.Title;
            if (!string.IsNullOrEmpty(title))
            {
                visibleRowTitles.Add(title);
            }
        }

        return visibleRowTitles;
    }

    private void HideColumnByPropertyName(string propertyName)
    {
        DataGridColumn? column = FindColumnByPropertyName(propertyName);
        if (column != null)
        {
            column.Visibility = Visibility.Collapsed;
        }
    }

    private void ShowColumnByPropertyName(string propertyName)
    {
        DataGridColumn? column = FindColumnByPropertyName(propertyName);
        if (column != null)
        {
            column.Visibility = Visibility.Visible;
        }
    }

    private DataGridColumn? FindColumnByPropertyName(string propertyName)
    {
        return _dataGrid.Columns.FirstOrDefault(c => (c.Header as string)?.Equals(propertyName, StringComparison.OrdinalIgnoreCase) == true);
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

public class AsyncCollectionToStringConverter<T>(MetadataAccessor<T> accessor) : IValueConverter
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
        return value is bool boolValue ? boolValue ? "✅" : "❌" : "❌";
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
