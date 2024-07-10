using GameManager.Core.Data;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

public static class DataGridHelper
{
    public static readonly List<string> AutoHeaders = [];

    private static void ClearDataGridItems(DataGrid dataGrid)
    {
        dataGrid.Items.Clear();
    }

    private static void SetDataGridItemSource(DataGrid dataGrid, IEnumerable itemSource)
    {
        dataGrid.ItemsSource = itemSource;
    }

    private static void AddColumnToDataGrid(DataGrid dataGrid, IList<Game> games, string columnToAdd)
    {
        DataGridTextColumn dataGridColumn = new()
        {
            Header = columnToAdd,
            Binding = new Binding(columnToAdd)
        };

        if (dataGrid.ItemsSource == null)
        {
            SetDataGridItemSource(dataGrid, games);
        }

        dataGrid.Columns.Add(dataGridColumn);
        dataGrid.Columns.Move(dataGrid.Columns.Count - 1, dataGrid.Columns.IndexOf(dataGridColumn));
    }

    public static void ShowHideColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        AutoHeaders.Add(e.Column.Header.ToString());

        HashSet<string> visibleColumns = ["Title", "Date", "Genres", "Developers", "Publishers", "Series"];
        e.Column.Visibility = visibleColumns.Contains(e.Column.Header.ToString()!) ? Visibility.Visible : Visibility.Collapsed;
    }

    public static void UpdateGameDataGrid(DataGrid dataGrid, GameData gameData)
    {
        IList<Game> games = gameData.GetGames();
        ClearDataGridItems(dataGrid);
        SetDataGridItemSource(dataGrid, games);
        dataGrid.AutoGeneratingColumn += ShowHideColumn!;
    }
}
