using GameManager.UI.Helpers;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        Logger.Info(LogClass.GameMgrUi, "Starting MainWindow");

        InitializeComponent();
        UpdateGameDataGrid();
        PopulateDataGridContextMenu();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Add());
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Import());
    }

    private void UpdateGameDataGrid()
    {
        DataGridHelper.UpdateGameDataGrid(GameDataGrid);
    }

    private void PopulateDataGridContextMenu()
    {
        ContextMenu contextMenu = GameDataGridContextMenu;

        IEnumerable<string> metadataCheckable = ["played", "finished", "completed"];
        foreach (string data in metadataCheckable)
        {
            MenuItem menuItem = new() { Header = $"Mark as {data}", IsCheckable = true };
            contextMenu.Items.Add(menuItem);
        }

        IEnumerable<string> metadata = ["Edit", "Delete"];
        foreach (string data in metadata)
        {
            MenuItem menuItem = new() { Header = $"{data}" };
            contextMenu.Items.Add(menuItem);
        }
    }
}
