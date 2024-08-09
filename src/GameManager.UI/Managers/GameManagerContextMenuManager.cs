using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UI.Common.Helpers;
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
    private ContextMenu _contextMenu = null!;

    public GameManagerContextMenuManager(GameManagerWindow mainWindow, GameMgrWindowViewModel viewModel)
    {
        _mainWindow = mainWindow;
        _viewModel = viewModel;
    }

    /// <summary>
    ///     Populates the game table context menu with MenuItems.
    /// </summary>
    public void PopulateDataGridContextMenu()
    {
        _contextMenu = new ContextMenu();

        AddMenuItem("Mark as played", _viewModel.MarkAsPlayedCommand, true);
        AddMenuItem("Mark as finished", _viewModel.MarkAsFinishedCommand, true);
        AddMenuItem("Mark as completed", _viewModel.MarkAsCompletedCommand, true);
        _contextMenu.Items.Add(new Separator());
        AddMenuItem("Edit", _viewModel.EditCommand, false, "Edit");
        AddMenuItem("Edit Tags", _viewModel.EditTagsCommand, false, "Tags");
        AddMenuItem("Delete", _viewModel.DeleteCommand, false, "Delete");

        _mainWindow.GameDataGrid.ContextMenu = _contextMenu;
        _mainWindow.GameDataGrid.ContextMenuOpening += GameDataGrid_ContextMenuOpening;
    }

    /// <summary>
    ///     Adds a menu item to the Game Table context menu.
    /// </summary>
    /// <param name="header">The menu item text to use.</param>
    /// <param name="command"> The command when the menu item gets clicked.</param>
    /// <param name="isCheckable">Whether the menu item can be checked or not.</param>
    /// <param name="image">The image name to use.</param>
    private void AddMenuItem(string header, ICommand command, bool isCheckable, string? image = null)
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
        _contextMenu.Items.Add(menuItem);
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
                Logger.Error(LogClass.GameMgrUiManagers, "ContextMenu is null");
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
