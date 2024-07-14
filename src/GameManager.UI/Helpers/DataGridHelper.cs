using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
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

        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Title", Binding = new Binding("Title") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Played", Binding = new Binding("HasPlayed") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Finished", Binding = new Binding("HasFinished") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Complete", Binding = new Binding("HasCompleted") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Genres", Binding = new Binding("Genres") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Developers", Binding = new Binding("Developers") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Publishers", Binding = new Binding("Publishers") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Series", Binding = new Binding("Series") });
        _dataGrid.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("ReleaseDateWw") });

        _dataGrid.ItemsSource = _games;
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
