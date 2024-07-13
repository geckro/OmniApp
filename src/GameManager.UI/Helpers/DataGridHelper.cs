using GameManager.Core.Data;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameManager.UI.Helpers;

public static class DataGridHelper
{
    public static void UpdateGameDataGrid(DataGrid dataGrid)
    {
        ICollection<Game> games = new DataManagerFactory().CreateData<Game>().ReadFromJson();

        dataGrid.AutoGenerateColumns = false;

        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Title", Binding = new Binding("Title") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Played", Binding = new Binding("HasPlayed") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Finished", Binding = new Binding("HasFinished") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Complete", Binding = new Binding("HasCompleted") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Genres", Binding = new Binding("Genres") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Developers", Binding = new Binding("Developers") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Publishers", Binding = new Binding("Publishers") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Series", Binding = new Binding("Series") });
        dataGrid.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("ReleaseDateWw") });

        dataGrid.Items.Clear();
        dataGrid.ItemsSource = games;
    }
}
