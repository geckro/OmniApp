using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
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
    private readonly JsonData<Game> _jsonData = new DataManagerFactory().CreateData<Game>();

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
        DataGridHelper.UpdateGameDataGrid(GameDataGrid, _jsonData);
    }

    private readonly Dictionary<string, (Action method, bool isCheckable)> _menuItems = new();

    /// <summary>
    ///     Populates the context menu of the Game DataGrid.
    /// </summary>
    private void PopulateDataGridContextMenu()
    {
        _menuItems.Add("Mark as played", (MarkAsPlayed, true));
        _menuItems.Add("Mark as finished", (MarkAsFinished, true));
        _menuItems.Add("Mark as completed", (MarkAsCompleted, true));
        _menuItems.Add("Edit", (Edit, false));
        _menuItems.Add("Delete", (Delete, false));

        GameDataGrid.ContextMenu ??= new ContextMenu();

        GameDataGrid.ContextMenu.Items.Clear();

        foreach (KeyValuePair<string, (Action method, bool isCheckable)> entry in _menuItems)
        {
            MenuItem menuItem = new() { Header = entry.Key };

            if (entry.Value.isCheckable)
            {
                menuItem.IsCheckable = true;
            }

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

    private Guid _gameId;

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem { Header: string header })
        {
            return;
        }

        if (_menuItems.TryGetValue(header, out (Action method, bool isCheckable) menuItemInfo))
        {
            if (GameDataGrid.ContextMenu is { DataContext: Game game })
            {
                _gameId = game.Id;

                // Invoke the method with gameId as an argument
                menuItemInfo.method.Invoke();
            }
            else
            {
                MessageBox.Show("DataContext is not a Game object.");
            }
        }
        else
        {
            MessageBox.Show($"Action not defined for '{header}'");
        }
    }

    private void MarkAsPlayed()
    {
        if (GameDataGrid.ContextMenu is { DataContext: Game game })
        {
            game.HasPlayed = true;
            _jsonData.UpdateAndWriteJson(_gameId, "HasPlayed", true);
            UpdateGameDataGrid();
        }
    }

    private void MarkAsFinished()
    {
        if (GameDataGrid.ContextMenu is { DataContext: Game game })
        {
            game.HasFinished = true;
            _jsonData.UpdateAndWriteJson(_gameId, "HasFinished", true);
            UpdateGameDataGrid();
        }
    }

    private void MarkAsCompleted()
    {
        if (GameDataGrid.ContextMenu is { DataContext: Game game })
        {
            game.HasCompleted = true;
            _jsonData.UpdateAndWriteJson(_gameId, "HasCompleted", true);
            UpdateGameDataGrid();
        }
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
