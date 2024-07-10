using GameManager.Core.Data;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class DataGridHelper
{
    public void UpdateGameDataGrid(DataGrid dataGrid, GameData gameData)
    {
        IList<Game> games = gameData.GetGames();

        dataGrid.Items.Clear();
        dataGrid.ItemsSource = games;
    }
}
