using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UiCommon;
using OmniApp.UiCommon.Helpers;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class EditEntryViewModel : ViewModelBase
{
    private readonly WindowHelper _windowHelper;
    private Action? _closeEditEntryWindow;

    public EditEntryViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
        RenameTitleCommand = new RelayCommand<Game>(RenameTitle);
    }

    public ICommand RenameTitleCommand { get; }

    public void SetCloseAction(Action? closeAction)
    {
        _closeEditEntryWindow = closeAction;
    }

    private void RenameTitle(Game? game)
    {
        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUi, "Attempted to edit the title of a null game");
            return;
        }

        _windowHelper.ShowDialogWindow<RenameDialog>(window =>
        {
            if (window is RenameDialog renameDialog)
            {
                renameDialog.SetCurrentGame(game);

                Logger.Info(LogClass.GameMgrUi, $"{renameDialog.WasRenamed}");

                renameDialog.Closed += (_, _) =>
                {
                    _closeEditEntryWindow?.Invoke();
                };
            }
            else
            {
                Logger.Error(LogClass.GameMgrUi, $"Expected RenameDialog window, got {window.GetType().Name}");
            }
        });
    }
}
