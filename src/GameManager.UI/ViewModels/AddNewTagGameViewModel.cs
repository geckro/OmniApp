using GameManager.UI.Windows;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class AddNewTagGameViewModel : ViewModelBase
{
    private readonly WindowHelper _windowHelper;

    public AddNewTagGameViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
        InitializeCommands();
    }

    public string? GameId { get; private set; }
    public ICommand PickGameCommand { get; private set; } = null!;

    private void InitializeCommands()
    {
        PickGameCommand = new RelayCommand<object>(_ => PickGame());
    }

    private void PickGame()
    {
        _windowHelper.ShowDialogWindow<PickGameDialog>(window =>
        {
            window.Closed += (_, _) =>
            {
                PickGameViewModel? viewModel = (PickGameViewModel)window.DataContext;
                string? gameId = viewModel.GameId;

                if (gameId != null)
                {
                    GameId = gameId;
                }
            };
        });
    }
}
