using GameManager.Core.Data;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class DataGridHelper
{
    public void UpdateGameDataGrid(DataGrid dataGrid, GameData gameData)
    {
        ICollection<Game> games = gameData.ReadFromJson();

        dataGrid.Items.Clear();
        dataGrid.ItemsSource = games;
    }
}
