using GameManager.Core.Data;
using GameManager.UI.Helpers;
using OmniApp.Common.Logging;
using System.Windows;

namespace GameManager.UI.Windows;

public partial class MainWindow
{
    private readonly DataGridHelper _dataGridHelper = new();

    public MainWindow()
    {
        Logger.Info(LogClass.GameMgrUi, "Starting MainWindow");

        InitializeComponent();

        UpdateGameDataGrid();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Add());
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Import());
    }

    public void UpdateGameDataGrid()
    {
        _dataGridHelper.UpdateGameDataGrid(GameDataGrid);
    }
}
