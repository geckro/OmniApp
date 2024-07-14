using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

/// <summary>
///     Helper for the DataGrid control.
/// </summary>
public static class DataGridHelper
{
    /// <summary>
    ///     Updates the main Game DataGrid on the MainWindow
    /// </summary>
    /// <param name="dataGrid">The Game DataGrid</param>
    /// <param name="gameData">Game data from an instance of DataFactoryManager</param>
    public static void UpdateGameDataGrid(DataGrid dataGrid, JsonData<Game> gameData)
    {
        ICollection<Game> games = gameData.ReadFromJson();

        dataGrid.AutoGenerateColumns = false;

        dataGrid.Columns.Clear();

        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Title", Binding = new Binding("Title") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Played", Binding = new Binding("HasPlayed") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Finished", Binding = new Binding("HasFinished") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Complete", Binding = new Binding("HasCompleted") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Genres", Binding = new Binding("Genres") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Developers", Binding = new Binding("Developers") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Publishers", Binding = new Binding("Publishers") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Series", Binding = new Binding("Series") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("ReleaseDateWw") });

        dataGrid.ItemsSource = games;
    }
}
