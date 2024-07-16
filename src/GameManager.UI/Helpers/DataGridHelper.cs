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
    private ICollection<Game> _games = [];

    /// <summary>
    ///     Populates the main Game DataGrid on the MainGameWindow.
    /// </summary>
    /// <param name="dataGrid">The Game DataGrid.</param>
    /// <param name="gameMetadataAccessor">Game data from an instance of DataFactoryManager.</param>
    public async Task PopulateGameDataGridAsync(DataGrid dataGrid, IMetadataAccessor<Game> gameMetadataAccessor)
    {
        _dataGrid = dataGrid;
        _games = await Task.Run(gameMetadataAccessor.LoadMetadataCollection);

        _dataGrid.AutoGenerateColumns = false;
        _dataGrid.Columns.Clear();

        ConfigureGameDataGridColumns();
        _dataGrid.ItemsSource = _games;
    }

    /// <summary>
    ///     Refreshes the main Game DataGrid on the MainGameWindow.
    /// </summary>
    /// <param name="gameMetadataAccessor">Game data from an instance of DataFactoryManager.</param>
    public async Task RefreshGameDataGridAsync(IMetadataAccessor<Game> gameMetadataAccessor)
    {
        _games = await Task.Run(gameMetadataAccessor.LoadMetadataCollection);
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
        AddTextColumn("Title", nameof(Game.Title));
        AddTrueFalseColumn("Played", nameof(Game.HasPlayed));
        AddTrueFalseColumn("Finished", nameof(Game.HasFinished));
        AddTrueFalseColumn("Complete", nameof(Game.HasCompleted));
        AddTextColumn("Genres", nameof(Game.Genres));
        AddTextColumn("Developers", nameof(Game.Developers));
        AddTextColumn("Publishers", nameof(Game.Publishers));
        AddTextColumn("Series", nameof(Game.Series));
        AddTextColumn("Date", nameof(Game.ReleaseDateWw));
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
