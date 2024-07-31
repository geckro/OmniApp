using OmniApp.UiCommon;
using OmniApp.UiCommon.Helpers;
using System.Windows.Input;

namespace OmniApp.UI.ViewModels;

public class MainWindowViewModel
{
    private readonly WindowHelper _windowHelper;

    public MainWindowViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
        OpenGameManagerCommand = new RelayCommand<object>(_ => OpenGameManager());
        OpenFinanceManagerCommand = new RelayCommand<object>(_ => OpenFinanceManager());
    }

    public ICommand OpenGameManagerCommand { get; }
    public ICommand OpenFinanceManagerCommand { get; }

    private void OpenGameManager()
    {
        _windowHelper.ShowDialogWindow<GameManager.UI.Windows.MainGameWindow>();
    }

    private void OpenFinanceManager()
    {
        _windowHelper.ShowDialogWindow<FinancialManager.UI.Windows.MainWindow>();
    }
}
