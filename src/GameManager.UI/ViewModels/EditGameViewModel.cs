using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class EditGameViewModel : ViewModelBase
{
    private readonly WindowHelper _windowHelper;
    private Action? _closeEditEntryWindow;
    private readonly RefreshManager _refreshManager;
    private string _currentSelectedItem = null!;
    private string? _currentCategory;
    private readonly MetadataAccessor<Game> _gameAcc;
    private Game? _gameData;

    public EditGameViewModel(WindowHelper windowHelper, MetadataAccessor<Game> gameAcc, GameTableHelper gameTableHelper)
    {
        _windowHelper = windowHelper;
        _gameAcc = gameAcc;
        _refreshManager = new RefreshManager(gameTableHelper);
        InitializeCommands();
    }

    public void SetGameData(Game? game)
    {
        _gameData = game;
    }

    public void SetSelectedItem(string? item)
    {
        if (item == null)
        {
            return;
        }

        _currentSelectedItem = item;
        Logger.Debug(LogClass.GameMgrUiViewModels, $"Current selected item {_currentSelectedItem}");
    }

    private void InitializeCommands()
    {
        RenameTitleCommand = new RelayCommand<Game>(RenameTitle);
        RenameCurrentItemCommand = new RelayCommand<Game>(RenameItem);
        AddNewItemToGameCommand = new RelayCommand<object>(AddNewItem);
        DeleteCurrentItemCommand = new RelayCommand<Game>(DeleteCurrentItem);
    }

    public ICommand RenameTitleCommand { get; private set; } = null!;
    public ICommand RenameCurrentItemCommand { get; private set; } = null!;
    public ICommand AddNewItemToGameCommand { get; private set; } = null!;
    public ICommand DeleteCurrentItemCommand { get; private set; } = null!;

    public void SetCloseAction(Action? closeAction)
    {
        _closeEditEntryWindow = closeAction;
    }

    private void RenameTitle(Game? game)
    {
        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUiViewModels, "Attempted to edit the title of a null game");
            return;
        }

        _windowHelper.ShowDialogWindow<RenameDialog>(window =>
        {
            if (window is RenameDialog)
            {
                window.SetCurrentGame(game);

                Logger.Info(LogClass.GameMgrUiViewModels, $"{window.WasRenamed}");

                window.Closed += (_, _) =>
                {
                    _closeEditEntryWindow?.Invoke();
                };
            }
            else
            {
                Logger.Error(LogClass.GameMgrUiViewModels, $"Expected RenameDialog window, got {window.GetType().Name}");
            }
        });
    }

    private void RenameItem(Game? game)
    {
        Logger.Debug(LogClass.GameMgrUiViewModels, "RenameItem called.");
        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUiViewModels, "Attempted to edit an item of a null game");
            return;
        }
    }

    private void AddNewItem(object? parameter)
    {
        if (parameter is not ContextMenu contextMenu)
        {
            Logger.Error(LogClass.GameMgrUiViewModels, $"Expected ContextMenu, got: {parameter?.GetType().FullName ?? "null"}");
            return;
        }

        if (contextMenu.PlacementTarget is not ListBox listBox || _gameData == null)
        {
            Logger.Error(LogClass.GameMgrUiViewModels, "Invalid parameter or null game");
            return;
        }

        _currentCategory = listBox.Name.Replace("ListBox", "");
        Logger.Debug(LogClass.GameMgrUiViewModels, $"Current category set to {_currentCategory}");

        _windowHelper.ShowDialogWindow<AddMetadataDialog>(window =>
        {
            if (window is not AddMetadataDialog)
            {
                return;
            }

            window.SetCurrentGame(_gameData);
            window.SetCurrentCategory(GetClickedCategory());
            window.Closed += (_, _) => _ = _refreshManager.RefreshControls(RefreshOptions.DataGrid);
        });
    }

    private string GetClickedCategory()
    {
        return _currentCategory ?? throw new InvalidOperationException("No category selected");
    }

    private void DeleteCurrentItem(Game? game)
    {
        if (game == null || string.IsNullOrEmpty(_currentSelectedItem))
        {
            return;
        }
    }
}
