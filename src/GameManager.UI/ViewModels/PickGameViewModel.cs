using OmniApp.Common.Logging;
using OmniApp.UI.Common;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class PickGameViewModel : ViewModelBase
{
    private Action? _closePickGameWindow;

    public PickGameViewModel()
    {
        ListBoxSelectedItems = [];
        InitializeCommands();
    }

    public ICommand OkCommand { get; private set; } = null!;
    public ICommand CancelCommand { get; private set; } = null!;

    public string? GameId { get; private set; }

    public ObservableCollection<ListBoxItem> ListBoxSelectedItems { get; }
    
    private void InitializeCommands()
    {
        OkCommand = new RelayCommand<object>(_ => OnButtonOk());
        CancelCommand = new RelayCommand<object>(_ => OnButtonCancel());
    }

    private void OnButtonCancel()
    {
        GameId = null;
        _closePickGameWindow?.Invoke();
    }

    private void OnButtonOk()
    {
        if (ListBoxSelectedItems.Count > 1)
        {
            Logger.Error(LogClass.GameMgrUi, "Multiple games have been picked, somehow. Closing the window.");
            GameId = null;
            _closePickGameWindow?.Invoke();
        }

        ListBoxItem game = ListBoxSelectedItems[0];

        Guid gameId = (Guid)game.Tag;

        GameId = gameId.ToString();

        _closePickGameWindow?.Invoke();
    }

    public void SetCloseAction(Action? closeAction)
    {
        _closePickGameWindow = closeAction;
    }
}
