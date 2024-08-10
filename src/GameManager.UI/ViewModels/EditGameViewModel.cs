using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class EditGameViewModel : ViewModelBase
{
    private readonly WindowHelper _windowHelper;
    private Action? _closeEditEntryWindow;
    private string _currentSelectedItem = null!;
    private readonly MetadataAccessor<Game> _gameAcc;

    public EditGameViewModel(WindowHelper windowHelper, MetadataAccessor<Game> gameAcc)
    {
        _windowHelper = windowHelper;
        _gameAcc = gameAcc;
        InitializeCommands();
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
        AddNewItemToGameCommand = new RelayCommand<Game>(AddNewItem);
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

    private void AddNewItem(Game? game)
    {
        Logger.Debug(LogClass.GameMgrUiViewModels, "AddNewItem called.");

        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUiViewModels, "Attempted to add a new item of a null game");
            return;
        }
    }

    private void DeleteCurrentItem(Game? game)
    {
        Logger.Debug(LogClass.GameMgrUiViewModels, "DeleteCurrentItem called.");

        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUiViewModels, "Attempted to delete an item of a null game");
            return;
        }
    }
}
