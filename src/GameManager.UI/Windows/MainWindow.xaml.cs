using GameManager.UI.Helpers;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

    private Dictionary<string, Action> _menuItems = new();

    /// <summary>
    ///     Populates the context menu of the Game DataGrid.
    /// </summary>
    private void PopulateDataGridContextMenu()
    {
        _menuItems.Add("Mark as played", MarkAsPlayed);
        _menuItems.Add("Mark as finished", MarkAsFinished);
        _menuItems.Add("Mark as completed", MarkAsCompleted);
        _menuItems.Add("Edit", Edit);
        _menuItems.Add("Delete", Delete);

        GameDataGrid.ContextMenu ??= new ContextMenu();

        GameDataGrid.ContextMenu.Items.Clear();

        IEnumerable<string> metadataCheckable = ["played", "finished", "completed"];
        foreach (string data in metadataCheckable)
        {
            MenuItem menuItem = new() { Header = $"Mark as {data}", IsCheckable = true };
            menuItem.Click += MenuItem_Click;
            GameDataGrid.ContextMenu.Items.Add(menuItem);
        }

        IEnumerable<string> metadata = ["Edit", "Delete"];
        foreach (string data in metadata)
        {
            MenuItem menuItem = new() { Header = $"{data}" };
            menuItem.Click += MenuItem_Click;
            GameDataGrid.ContextMenu.Items.Add(menuItem);
        }

        GameDataGrid.ContextMenuOpening += GameDataGrid_ContextMenuOpening;
    }

    private void GameDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
    {
        if (e.OriginalSource is DependencyObject source)
        {
            DataGridRow? dataGridRow = FindAncestor<DataGridRow>(source);

            if (dataGridRow != null)
            {
                if (GameDataGrid.ContextMenu != null)
                {
                    GameDataGrid.ContextMenu.DataContext = dataGridRow.DataContext;
                }
                else
                {
                    throw new Exception("ContextMenu is null");
                }
            }
            else
            {
                throw new Exception("DataGridRow not found");
            }
        }
        else
        {
            throw new Exception("OriginalSource is not a DependencyObject");
        }
    }

    private static T? FindAncestor<T>(DependencyObject? current) where T : DependencyObject
    {
        while (current != null)
        {
            if (current is T ancestor)
            {
                return ancestor;
            }

            current = VisualTreeHelper.GetParent(current);
        }

        return null;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem { Header: string header })
        {
            return;
        }

        if (_menuItems.TryGetValue(header, out Action? value))
        {
            value.Invoke();
        }
        else
        {
            MessageBox.Show($"Action not defined for '{header}'");
        }
    }

    private void MarkAsPlayed()
    {
        MessageBox.Show("MarkAsPlayed");
    }

    private void MarkAsFinished()
    {
        MessageBox.Show("MarkAsFinished");
    }

    private void MarkAsCompleted()
    {
        MessageBox.Show("MarkAsCompleted");
    }

    private void Edit()
    {
        MessageBox.Show("Edit");
    }

    private void Delete()
    {
        MessageBox.Show("Delete");
    }
}
