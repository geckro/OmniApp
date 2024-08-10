using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.Common.Logging;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

/// <summary>
///     Helper for the Game DataGrid control on the MainGameWindow.
/// </summary>
public class GameTableHelper
{
    private DataGrid _dataGrid = null!;
    private ICollection<Game> _gameCollection = [];
    private readonly MetadataAccessor<Game> _gameAcc;
    private readonly MetadataAccessor<Genre> _genreAcc;
    private readonly MetadataAccessor<Developer> _developerAcc;
    private readonly MetadataAccessor<Publisher> _publisherAcc;
    private readonly MetadataAccessor<Platform> _platformAcc;
    private readonly MetadataAccessor<Series> _seriesAcc;

    /// <summary>
    ///     Helper for the Game DataGrid control on the MainGameWindow.
    /// </summary>
    public GameTableHelper(MetadataAccessor<Game> gameAcc,
            MetadataAccessor<Genre> genreAcc,
            MetadataAccessor<Developer> developerAcc,
            MetadataAccessor<Publisher> publisherAcc,
            MetadataAccessor<Platform> platformAcc,
            MetadataAccessor<Series> seriesAcc)
    {
        _gameAcc = gameAcc;
        _genreAcc = genreAcc;
        _developerAcc = developerAcc;
        _publisherAcc = publisherAcc;
        _platformAcc = platformAcc;
        _seriesAcc = seriesAcc;
    }

    /// <summary>
    ///     Populates the main Game DataGrid on the GameManagerWindow.
    /// </summary>
    /// <param name="dataGrid">The Game DataGrid.</param>
    public async Task PopulateGameTableAsync(DataGrid dataGrid)
    {
        Logger.Debug(LogClass.GameMgrUiHelpers, "Populating the game table...");

        try
        {
            _dataGrid = dataGrid;
            ConfigureGameDataGrid();
            await RefreshGameTableAsync();

            Logger.Info(LogClass.GameMgrUiHelpers, "Game DataGrid successfully populated.");
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUiHelpers, $"Error populating Game DataGrid: {ex.Message}");
        }
    }

    /// <summary>
    ///     Refreshes the main Game DataGrid on the GameManagerWindow.
    /// </summary>
    public async Task RefreshGameTableAsync()
    {
        Logger.Debug(LogClass.GameMgrUiHelpers, "Refreshing the game table...");

        try
        {
            _gameCollection = await Task.Run(() => _gameAcc.LoadMetadata(true));
            await AnimateOpacityAsync(1.0, 0.5, 50);

            _dataGrid.ItemsSource = null;
            _dataGrid.ItemsSource = _gameCollection;

            if (_dataGrid.ItemsSource != null)
            {
                Logger.Info(LogClass.GameMgrUiHelpers, "Game DataGrid successfully refreshed.");
            }
            else
            {
                Logger.Warning(LogClass.GameMgrUiHelpers, "Game DataGrid did not refresh properly as ItemsSource is null.");
            }

            await Task.Delay(250);
            await AnimateOpacityAsync(0.5, 1.0, 50);
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUiHelpers, $"Error refreshing Game DataGrid: {ex.Message}");
        }
    }

    private async Task AnimateOpacityAsync(double minOpacity, double maxOpacity, int stepDuration)
    {
        double step = 0.1 * Math.Sign(maxOpacity - minOpacity);
        double currentOpacity = minOpacity;

        while (Math.Abs(currentOpacity - maxOpacity) > 0.1)
        {
            _dataGrid.Opacity = currentOpacity;
            await Task.Delay(stepDuration);
            currentOpacity += step;
        }

        _dataGrid.Opacity = maxOpacity;
    }

    private void ConfigureGameDataGrid()
    {
        Logger.Debug(LogClass.GameMgrUiHelpers, "Running ConfigureGameDataGrid.");

        _dataGrid.AutoGenerateColumns = false;
        _dataGrid.Columns.Clear();

        DataGridColumn[] columns =
        [
                new DataGridTextColumn { Header = "Title", Binding = new Binding(nameof(Game.Title)) },
                CreateBooleanColumn("Played", nameof(Game.HasPlayed)),
                CreateBooleanColumn("Finished", nameof(Game.HasFinished)),
                CreateBooleanColumn("Complete", nameof(Game.HasCompleted)),
                CreateAsyncColumn("Platforms", nameof(Game.Platforms),
                        new AsyncCollectionToStringConverter<Platform>(_platformAcc)),
                CreateAsyncColumn("Genres", nameof(Game.Genres),
                        new AsyncCollectionToStringConverter<Genre>(_genreAcc)),
                CreateAsyncColumn("Developers", nameof(Game.Developers),
                        new AsyncCollectionToStringConverter<Developer>(_developerAcc)),
                CreateAsyncColumn("Publishers", nameof(Game.Publishers),
                        new AsyncCollectionToStringConverter<Publisher>(_publisherAcc)),
                CreateAsyncColumn("Series", nameof(Game.Series),
                        new AsyncCollectionToStringConverter<Series>(_seriesAcc)),
                new DataGridTextColumn
                {
                        Header = "Date",
                        Binding = new Binding(nameof(Game.ReleaseDateWw)) { StringFormat = "d" }
                }
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
            if (_dataGrid.ItemContainerGenerator.ContainerFromItem(item) is not DataGridRow
                {
                        Visibility: Visibility.Visible
                } row)
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
        return _dataGrid.Columns.FirstOrDefault(c =>
                (c.Header as string)?.Equals(propertyName, StringComparison.OrdinalIgnoreCase) == true);
    }

    private static DataGridTextColumn CreateAsyncColumn(string header, string propertyName, IValueConverter converter)
    {
        return new DataGridTextColumn
        {
                Header = header, Binding = new Binding(propertyName) { Converter = converter }
        };
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
                                new Binding(propertyName) { Converter = new BooleanYesNoConverter() })
                }
        };
    }

    public async Task FilterGameTableAsync(FilterHelper filterHelper)
    {
        Dictionary<string, List<string>> checkedFilters = filterHelper.GetCheckedFilters();

        IEnumerable<Game> filteredRows = await Task.Run(() =>
        {
            return _gameCollection.Where(game =>
            {
                foreach ((string key, List<string> values) in checkedFilters)
                {
                    bool isMatch = key switch
                    {
                            "ReleaseDateWw" => game.ReleaseDateWw.HasValue &&
                                               values.Contains(game.ReleaseDateWw.Value.Year.ToString()),
                            "Developers" => game.Developers != null && game.Developers.Any(dev =>
                                    values.Contains(_developerAcc.GetItemById(dev.Id)?.Name ?? string.Empty)),
                            "Publishers" => game.Publishers != null && game.Publishers.Any(pub =>
                                    values.Contains(_publisherAcc.GetItemById(pub.Id)?.Name ?? string.Empty)),
                            "Genres" => game.Genres != null && game.Genres.Any(pub =>
                                    values.Contains(_genreAcc.GetItemById(pub.Id)?.Name ?? string.Empty)),
                            "Platforms" => game.Platforms != null && game.Platforms.Any(pub =>
                                    values.Contains(_platformAcc.GetItemById(pub.Id)?.Name ?? string.Empty)),
                            "Series" => game.Series != null && game.Series.Any(pub =>
                                    values.Contains(_seriesAcc.GetItemById(pub.Id)?.Name ?? string.Empty)),
                            _ => true
                    };

                    if (!isMatch)
                    {
                        return false;
                    }
                }

                return true;
            });
        });

        _dataGrid.ItemsSource = filteredRows.ToList();
    }
}

public class AsyncCollectionToStringConverter<T>(MetadataAccessor<T> accessor) : IValueConverter where T : IMetadata
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not IEnumerable<IMetadata> collection
                ? string.Empty
                : GetNamedCollectionAsync(collection).GetAwaiter().GetResult();
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
    public static FrameworkElementFactory AddBinding(this FrameworkElementFactory factory,
            DependencyProperty property,
            BindingBase binding)
    {
        factory.SetBinding(property, binding);
        return factory;
    }
}
