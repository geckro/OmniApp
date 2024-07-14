using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

/// <summary>
///     Helper for the DataGrid control.
/// </summary>
public class DataGridHelper
{
    private ICollection<Game> _games = [];
    private DataGrid _dataGrid = null!;

    /// <summary>
    ///     Populates the main Game DataGrid on the MainWindow.
    /// </summary>
    /// <param name="dataGrid">The Game DataGrid.</param>
    /// <param name="gameData">Game data from an instance of DataFactoryManager.</param>
    public void PopulateGameDataGrid(DataGrid dataGrid, JsonData<Game> gameData)
    {
        _games = gameData.ReadFromJson();
        _dataGrid = dataGrid;

        _dataGrid.AutoGenerateColumns = false;

        _dataGrid.Columns.Clear();

        AddTextColumn("Title", "Title");
        AddTrueFalseColumn("Played", "HasPlayed");
        AddTrueFalseColumn("Finished", "HasFinished");
        AddTrueFalseColumn("Complete", "HasCompleted");
        AddTextColumn("Genres", "Genres");
        AddTextColumn("Developers", "Developers");
        AddTextColumn("Publishers", "Publishers");
        AddTextColumn("Series", "Series");
        AddTextColumn("Date", "ReleaseDateWw");

        _dataGrid.ItemsSource = _games;
    }

    private void AddTextColumn(string header, string bindingPath)
    {
        _dataGrid.Columns.Add(new DataGridTextColumn
        {
            Header = header,
            Binding = new Binding(bindingPath)
        });
    }

    private void AddTrueFalseColumn(string header, string propertyName)
    {
        _dataGrid.Columns.Add(CreateTrueFalseColumn(header, propertyName));
    }

    private static DataGridTemplateColumn CreateTrueFalseColumn(string header, string propertyName)
    {
        DataGridTemplateColumn column = new()
        {
            Header = header
        };

        DataTemplate template = new(typeof(TextBlock))
        {
            VisualTree = new FrameworkElementFactory(typeof(TextBlock))
        };

        template.VisualTree.SetBinding(TextBlock.TextProperty, new Binding(propertyName)
        {
            Converter = new BooleanYesNoRenameConverter()
        });

        column.CellTemplate = template;

        return column;
    }

    /// <summary>
    /// Refreshes the main Game DataGrid on the MainWindow.
    /// </summary>
    /// <param name="gameData">Game data from an instance of DataFactoryManager.</param>
    public void RefreshGameDataGrid(JsonData<Game> gameData)
    {
        _games = gameData.ReadFromJson();
        _dataGrid.ItemsSource = _games;
    }
}

/// <summary>
/// Converter to display True/False as Yes or No.
/// </summary>
public class BooleanYesNoRenameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "Yes" : "No";
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
