using GameManager.UI.Helpers;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

/// <summary>
///     Logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    /// <summary>
    ///     Initializes a new instance of the MainWindow class.
    /// </summary>
    public MainWindow()
    {
        Logger.Info(LogClass.GameMgrUi, "Starting MainWindow");

        InitializeComponent();
        UpdateGameDataGrid();
        PopulateDataGridContextMenu();
    }

    /// <summary>
    ///     Handles click event of the Add... Button
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">RoutedEventArgs event data</param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Add());
    }

    /// <summary>
    ///     Handles click event of the Import... Button
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">RoutedEventArgs event data</param>
    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Import());
    }

    /// <summary>
    ///     Updates the Game DataGrid.
    /// </summary>
    private void UpdateGameDataGrid()
    {
        DataGridHelper.UpdateGameDataGrid(GameDataGrid);
    }

    /// <summary>
    ///     Populates the context menu of the Game DataGrid.
    /// </summary>
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
