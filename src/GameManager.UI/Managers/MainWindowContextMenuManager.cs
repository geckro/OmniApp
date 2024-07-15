using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GameManager.UI.Managers;

public class MainWindowContextMenuManager(MainWindow mainWindow, DataGridHelper dataGridHelper, IMetadataAccessor<Game> metadataAccessor)
{
    private readonly Dictionary<string, (Action method, bool isCheckable)> _menuItems = new();

    /// <summary>
    ///     Populates the context menu of the Game DataGrid.
    /// </summary>
    public void PopulateDataGridContextMenu()
    {
        _menuItems.Add("Mark as played", (MarkAsPlayed, true));
        _menuItems.Add("Mark as finished", (MarkAsFinished, true));
        _menuItems.Add("Mark as completed", (MarkAsCompleted, true));
        _menuItems.Add("Edit", (Edit, false));
        _menuItems.Add("Delete", (Delete, false));

        mainWindow.GameDataGrid.ContextMenu ??= new ContextMenu();

        mainWindow.GameDataGrid.ContextMenu.Items.Clear();

        foreach (KeyValuePair<string, (Action method, bool isCheckable)> entry in _menuItems)
        {
            MenuItem menuItem = new() { Header = entry.Key };

            if (entry.Value.isCheckable)
            {
                menuItem.IsCheckable = true;
            }

            menuItem.Click += MenuItem_Click;
            mainWindow.GameDataGrid.ContextMenu.Items.Add(menuItem);
        }

        mainWindow.GameDataGrid.ContextMenuOpening += GameDataGrid_ContextMenuOpening;
    }

    private void GameDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
    {
        if (e.OriginalSource is DependencyObject source)
        {
            if (source is ScrollViewer)
            {
                e.Handled = true;
                return;
            }

            DataGridColumnHeader? dataGridColumn = FindAncestor<DataGridColumnHeader>(source);

            if (dataGridColumn != null)
            {
                e.Handled = true;
                return;
            }

            DataGridRow? dataGridRow = FindAncestor<DataGridRow>(source);

            if (dataGridRow != null)
            {
                if (mainWindow.GameDataGrid.ContextMenu != null)
                {
                    mainWindow.GameDataGrid.ContextMenu.DataContext = dataGridRow.DataContext;
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
            if (mainWindow.GameDataGrid.ContextMenu is { DataContext: Game game })
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
        if (mainWindow.GameDataGrid.ContextMenu is { DataContext: Game game })
        {
            game.HasPlayed = true;
            metadataAccessor.UpdateItemAndSave(_gameId, "HasPlayed", true);
            dataGridHelper.RefreshGameDataGrid(metadataAccessor);
        }
    }

    private void MarkAsFinished()
    {
        if (mainWindow.GameDataGrid.ContextMenu is { DataContext: Game game })
        {
            game.HasFinished = true;
            metadataAccessor.UpdateItemAndSave(_gameId, "HasFinished", true);
            dataGridHelper.RefreshGameDataGrid(metadataAccessor);
        }
    }

    private void MarkAsCompleted()
    {
        if (mainWindow.GameDataGrid.ContextMenu is { DataContext: Game game })
        {
            game.HasCompleted = true;
            metadataAccessor.UpdateItemAndSave(_gameId, "HasCompleted", true);
            dataGridHelper.RefreshGameDataGrid(metadataAccessor);
        }
    }

    private void Edit()
    {
        if (mainWindow.GameDataGrid.ContextMenu is { DataContext: Game game })
        {
            WindowHelper.ShowWindow(new EditEntry(game, metadataAccessor, dataGridHelper));
        }
    }

    private void Delete()
    {
        MessageBox.Show("Delete");
    }
}
