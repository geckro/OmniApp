using OmniApp.Common.WindowsUtils;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Windows.Input;

namespace FinancialManager.UI.ViewModels;

public class FinanceManagerViewModel
{
    private readonly WindowHelper _windowHelper;

    public FinanceManagerViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;

        InitializeCommands();
    }

    public ICommand AddFinanceCommand { get; private set; } = null!;
    public ICommand ModifyFinanceCategoriesCommand { get; private set; } = null!;
    public ICommand RefreshDataGridCommand { get; private set; } = null!;
    public ICommand OpenFinancesJsonCommand { get; private set; } = null!;
    public ICommand OpenPreferencesCommand { get; private set; } = null!;

    private void InitializeCommands()
    {
        AddFinanceCommand = new RelayCommand<object>(_ => throw new NotImplementedException());
        ModifyFinanceCategoriesCommand = new RelayCommand<object>(_ => throw new NotImplementedException());
        RefreshDataGridCommand = new RelayCommand<object>(_ => throw new NotImplementedException());
        OpenFinancesJsonCommand =
                new RelayCommand<object>(_ => FileHelper.OpenFileWithDefaultProgram(@"Data\FinanceMgr\finances.json"));
        OpenPreferencesCommand = new RelayCommand<object>(_ => throw new NotImplementedException());
    }
}
