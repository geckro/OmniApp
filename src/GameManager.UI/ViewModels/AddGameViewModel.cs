using GameManager.UI.Windows;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class AddGameViewModel : ViewModelBase
{
    private readonly WindowHelper _windowHelper;

    public AddGameViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
        InitializeCommands();
    }

    public ICommand PickDateCommand { get; private set; } = null!;

    private void InitializeCommands()
    {
        PickDateCommand = new RelayCommand<object>(_ => PickDate());
    }

    private void PickDate()
    {
        _windowHelper.ShowDialogWindow<GameDateSetterWindow>();
    }
}
