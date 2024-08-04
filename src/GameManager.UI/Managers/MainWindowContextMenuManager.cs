using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace GameManager.UI.Managers;

public class MainWindowContextMenuManager
{
    private readonly GameManagerWindow _mainWindow;
    private readonly MainGameWindowViewModel _viewModel;

    public MainWindowContextMenuManager(GameManagerWindow mainWindow, MainGameWindowViewModel viewModel)
    {
        _mainWindow = mainWindow;
        _viewModel = viewModel;
    }

    public void PopulateDataGridContextMenu()
    {
        ContextMenu contextMenu = new();

        AddMenuItem(contextMenu, "Mark as played", _viewModel.MarkAsPlayedCommand, true);
        AddMenuItem(contextMenu, "Mark as finished", _viewModel.MarkAsFinishedCommand, true);
        AddMenuItem(contextMenu, "Mark as completed", _viewModel.MarkAsCompletedCommand, true);
        contextMenu.Items.Add(new Separator());
        AddMenuItem(contextMenu, "Edit", _viewModel.EditCommand, false);
        AddMenuItem(contextMenu, "Edit Tags", _viewModel.EditTagsCommand, false);
        AddMenuItem(contextMenu, "Delete", _viewModel.DeleteCommand, false);

        _mainWindow.GameDataGrid.ContextMenu = contextMenu;
        _mainWindow.GameDataGrid.ContextMenuOpening += GameDataGrid_ContextMenuOpening;
    }

    private static void AddMenuItem(ContextMenu contextMenu, string header, ICommand command, bool isCheckable)
    {
        MenuItem menuItem = new() { Header = header, Command = command, IsCheckable = isCheckable };
        menuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding());
        contextMenu.Items.Add(menuItem);
    }

    private void GameDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
    {
        if (e.OriginalSource is not DependencyObject source)
        {
            e.Handled = true;
            return;
        }

        if (source is ScrollViewer || FindAncestor<DataGridColumnHeader>(source) != null)
        {
            e.Handled = true;
            return;
        }

        DataGridRow? dataGridRow = FindAncestor<DataGridRow>(source);
        if (dataGridRow != null)
        {
            if (_mainWindow.GameDataGrid.ContextMenu != null)
            {
                foreach (MenuItem item in _mainWindow.GameDataGrid.ContextMenu.Items.OfType<MenuItem>())
                {
                    item.DataContext = dataGridRow.DataContext;
                }
            }
            else
            {
                Logger.Error(LogClass.GameMgrUi, "ContextMenu is null");
                e.Handled = true;
            }
        }
        else
        {
            e.Handled = true;
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
