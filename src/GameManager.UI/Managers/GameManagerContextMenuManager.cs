using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameManager.UI.Managers;

public class GameManagerContextMenuManager
{
    private readonly GameManagerWindow _mainWindow;
    private readonly GameMgrWindowViewModel _viewModel;
    private ContextMenu _dataGridRowContextMenu = null!;
    private ContextMenu _dataGridHeaderContextMenu = null!;


    public GameManagerContextMenuManager(GameManagerWindow mainWindow, GameMgrWindowViewModel viewModel)
    {
        _mainWindow = mainWindow;
        _viewModel = viewModel;
    }

    /// <summary>
    ///     Populates the game table context menu with MenuItems.
    /// </summary>
    public void PopulateContextMenus()
    {
        PopulateGameTableRowContextMenu();
        PopulateGameTableHeaderContextMenu();

        _mainWindow.GameDataGrid.ContextMenuOpening += GameDataGrid_ContextMenuOpening;
    }

    private void PopulateGameTableRowContextMenu()
    {
        _dataGridRowContextMenu = new ContextMenu();

        AddMenuItem(_dataGridRowContextMenu, "Mark as played", _viewModel.MarkAsPlayedCommand, true);
        AddMenuItem(_dataGridRowContextMenu, "Mark as finished", _viewModel.MarkAsFinishedCommand, true);
        AddMenuItem(_dataGridRowContextMenu, "Mark as completed", _viewModel.MarkAsCompletedCommand, true);
        _dataGridRowContextMenu.Items.Add(new Separator());
        AddMenuItem(_dataGridRowContextMenu, "Copy Value", _viewModel.CopyValueCommand, false, "Copy");
        AddMenuItem(_dataGridRowContextMenu, "Edit", _viewModel.EditCommand, false, "Edit");
        AddMenuItem(_dataGridRowContextMenu, "Edit Tags", _viewModel.EditTagsCommand, false, "Tags");
        AddMenuItem(_dataGridRowContextMenu, "Delete", _viewModel.DeleteCommand, false, "Delete");
    }

    private void PopulateGameTableHeaderContextMenu()
    {
        _dataGridHeaderContextMenu = new ContextMenu();

        AddMenuItem(_dataGridHeaderContextMenu, "Sort A-Z", _viewModel.SortHeaderCommand, false, "Sort");
        AddMenuItem(_dataGridHeaderContextMenu, "Sort Z-A", _viewModel.SortHeaderCommand, false, "SortReverse");
        AddMenuItem(_dataGridHeaderContextMenu, "Show Columns", _viewModel.ShowColumnsCommand, false, "Preferences");
        AddMenuItem(_dataGridHeaderContextMenu, "Change Column Header Name", _viewModel.ChangeColumnHeaderName, false, "Edit");
    }

    /// <summary>
    ///     Adds a menu item to the Game Table context menu.
    /// </summary>
    /// <param name="menu">The ContextMenu to add the MenuItem to.</param>
    /// <param name="header">The menu item text to use.</param>
    /// <param name="command"> The command when the menu item gets clicked.</param>
    /// <param name="isCheckable">Whether the menu item can be checked or not.</param>
    /// <param name="image">The image name to use.</param>
    private void AddMenuItem(ContextMenu menu, string header, ICommand command, bool isCheckable, string? image = null)
    {
        MenuItem menuItem = new() { Header = header, Command = command, IsCheckable = isCheckable };
        if (image != null)
        {
            Image iconImage = new()
            {
                    Source = new BitmapImage(
                            new Uri($"pack://application:,,,/OmniApp.UI.Common;component/Images/{image}.png")),
            };
            RenderOptions.SetBitmapScalingMode(iconImage, BitmapScalingMode.HighQuality);
            menuItem.Icon = iconImage;
        }
        menuItem.SetBinding(MenuItem.CommandParameterProperty, new Binding());
        menu.Items.Add(menuItem);
    }

    private void GameDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
    {
        if (e.OriginalSource is not DependencyObject source)
        {
            e.Handled = true;
            return;
        }
        DataGridColumnHeader? header = FindAncestor<DataGridColumnHeader>(source);
        DataGridRow? row = FindAncestor<DataGridRow>(source);

        if (header != null)
        {
            _mainWindow.GameDataGrid.ContextMenu = _dataGridHeaderContextMenu;
            foreach (MenuItem item in _dataGridHeaderContextMenu.Items.OfType<MenuItem>())
            {
                item.DataContext = header.DataContext;
            }
        }
        else if (row != null)
        {
            _mainWindow.GameDataGrid.ContextMenu = _dataGridRowContextMenu;
            foreach (MenuItem item in _dataGridRowContextMenu.Items.OfType<MenuItem>())
            {
                item.DataContext = row.DataContext;
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
