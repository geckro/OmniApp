using GameManager.Core.Data;
using System.Collections;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class DataGridHelper
{
    private static DataGridHelper? _instance;

    private readonly HashSet<string> _visibleColumns =
        ["Title", "Date", "Genres", "Developers", "Publishers", "Series"];

    private DataGridHelper()
    {
    }

    public static DataGridHelper Instance => _instance ??= new DataGridHelper();

    public HashSet<string> AutoHeaders { get; } = [];

    private static void ClearDataGridItems(DataGrid dataGrid)
    {
        dataGrid.Items.Clear();
    }

    private static void SetDataGridItemSource(DataGrid dataGrid, IEnumerable itemSource)
    {
        dataGrid.ItemsSource = itemSource;
    }

    public void UpdateGameDataGrid(DataGrid dataGrid, GameData gameData)
    {
        IList<Game> games = gameData.GetGames();

        // show columns here

        ClearDataGridItems(dataGrid);
        SetDataGridItemSource(dataGrid, games);
    }
}
