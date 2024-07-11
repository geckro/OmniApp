using GameManager.Core.Data;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class DataGridHelper
{
    public void UpdateGameDataGrid(DataGrid dataGrid)
    {
        ICollection<Game> games = new DataManagerFactory().CreateData<Game>().ReadFromJson();

        dataGrid.Items.Clear();
        dataGrid.ItemsSource = games;
    }
}
