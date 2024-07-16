using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace GameManager.UI.Managers;

public class MainWindowContextMenuManager(MainGameWindow mainWindow, MainGameWindowViewModel viewModel)
{
    public void PopulateDataGridContextMenu()
    {
        ContextMenu contextMenu = new();

        AddMenuItem(contextMenu, "Mark as played", viewModel.MarkAsPlayedCommand, true);
        AddMenuItem(contextMenu, "Mark as finished", viewModel.MarkAsFinishedCommand, true);
        AddMenuItem(contextMenu, "Mark as completed", viewModel.MarkAsCompletedCommand, true);
        AddMenuItem(contextMenu, "Edit", viewModel.EditCommand, false);
        AddMenuItem(contextMenu, "Delete", viewModel.DeleteCommand, false);

        mainWindow.GameDataGrid.ContextMenu = contextMenu;
        mainWindow.GameDataGrid.ContextMenuOpening += GameDataGrid_ContextMenuOpening;
    }

    private void AddMenuItem(ContextMenu contextMenu, string header, ICommand command, bool isCheckable)
    {
        MenuItem menuItem = new()
        {
            Header = header,
            Command = command,
            IsCheckable = isCheckable
        };
        contextMenu.Items.Add(menuItem);
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
}
